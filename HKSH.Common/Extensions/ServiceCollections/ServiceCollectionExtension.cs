using HKSH.Common.Adapter;
using HKSH.Common.AuditLog;
using HKSH.Common.AutoMapper;
using HKSH.Common.Caching.Redis;
using HKSH.Common.Constants;
using HKSH.Common.CustomHealthChecks;
using HKSH.Common.Elastic;
using HKSH.Common.File;
using HKSH.Common.RabbitMQ;
using HKSH.Common.ServiceInvoker;
using HKSH.Common.ShareModel;
using HKSH.Common.ShareModel.Base;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using RestSharp;
using System.Reflection;

namespace HKSH.Common.Extensions.ServiceCollections;

/// <summary>
/// ServiceCollection Extension
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds the swagger.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            OpenApiSecurityScheme securityScheme = new()
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, Array.Empty<string>()}
            });

            string[] allDocPaths = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (string item in allDocPaths)
            {
                c.IncludeXmlComments(item, true);
            }
        });
        return services;
    }

    /// <summary>
    /// Adds the n log.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddNLog(this IServiceCollection services, ConfigurationManager configuration)
    {
        try
        {
            IConfigurationSection nLogConnetctSection = configuration.GetSection("NLog:targets:db:connectionString");
            nLogConnetctSection.Value = configuration.GetConnectionString("SqlServer");
            LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));

            services.AddLogging(configure =>
            {
                configure.AddNLog(LogManager.Configuration);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"NLog Connection failed: {ex.Message}", ex);
        }

        return services;
    }

    /// <summary>
    /// Registers the database context related.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterDbContextRelated<TContext>(this IServiceCollection services, ConfigurationManager configuration) where TContext : DbContext, IBasicDbContext
    {
        try
        {
            services.AddDbContextPool<TContext>(option => option.UseSqlServer(configuration.GetConnectionString("SqlServer")), poolSize: 64);
            services.AddScoped<TContext, TContext>();
            services.AddDatabase<TContext>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB Connection failed: {ex?.Message}", ex);
        }

        services.AddHealthChecks().AddCheck<DatabaseConnectionHealthCheck<TContext>>("SqlDatabase");

        return services;
    }

    /// <summary>
    /// Injects the i service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection InjectIService(this IServiceCollection services)
    {
        IEnumerable<TypeInfo>? types = Assembly.GetEntryAssembly()?.DefinedTypes.Where(a => typeof(IService).IsAssignableFrom(a));
        if (types != null)
        {
            foreach (TypeInfo? type in types)
            {
                if (type.IsAbstract || type.IsInterface)
                {
                    continue;
                }

                services.AddTransient(type);
            }
        }

        return services;
    }

    /// <summary>
    /// Adds all json files.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="env">The env.</param>
    /// <returns></returns>
    public static ConfigurationManager AddAllJsonFiles(this ConfigurationManager configuration, string env)
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        IEnumerable<string> jsonConfigs = executingAssembly.GetManifestResourceNames().Where(a => a.EndsWith(".json"));
        foreach (string? json in jsonConfigs)
        {
            Stream? stream = executingAssembly.GetManifestResourceStream(json);
            if (stream != null && stream.Length > 0)
            {
                configuration.AddJsonStream(stream);
            }
        }

        Assembly? entryAssembly = Assembly.GetEntryAssembly();
        IEnumerable<string>? entryJsonConfigs = entryAssembly?.GetManifestResourceNames().Where(a => a.EndsWith(".json"));
        if (entryJsonConfigs != null && entryJsonConfigs.Any())
        {
            foreach (string? json in entryJsonConfigs)
            {
                Stream? stream = entryAssembly?.GetManifestResourceStream(json);
                if (stream != null && stream.Length > 0)
                {
                    configuration.AddJsonStream(stream);
                }
            }
        }

        configuration.AddJsonFile("./Configmap/appsettings.configmap.common.json", optional: true, reloadOnChange: true);
        configuration.AddJsonFile("./Configmap/appsettings.configmap.nlogs.json", optional: true, reloadOnChange: true);
        configuration.AddJsonFile("./Secret/appsettings.secret.common.json", optional: true, reloadOnChange: true);
        configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        configuration.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

        return configuration;
    }

    /// <summary>
    /// Adds the key cloak.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddKeyCloak(this IServiceCollection services, ConfigurationManager configuration)
    {
        KeycloakAuthenticationOptions authenticationOptions = configuration.GetSection(KeycloakAuthenticationOptions.Section).Get<KeycloakAuthenticationOptions>();
        services.AddKeycloakAuthentication(authenticationOptions);

        KeycloakProtectionClientOptions authorizationOptions = configuration.GetSection(KeycloakProtectionClientOptions.Section).Get<KeycloakProtectionClientOptions>();
        services.AddAuthorization().AddKeycloakAuthorization(authorizationOptions);

        return services;
    }

    /// <summary>
    /// Adds the variable options.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddVariableOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<List<VariableOptions>>(configuration.GetSection(VariableOptions.SECTION));
        services.AddTransient<List<VariableOptions>>();
        return services;
    }

    /// <summary>
    /// Adds the minio options.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddMinioOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.SECTION));
        services.AddTransient<MinioOptions>();
        return services;
    }

    /// <summary>
    /// Adds the enable audit log options.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddEnableAuditLogOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<EnableAuditLogOptions>(configuration.GetSection(EnableAuditLogOptions.SECTION));
        services.AddTransient<EnableAuditLogOptions>();

        return services;
    }

    /// <summary>
    /// Adds the rabbit mq.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="rabbitMqOptions">The rabbit mq options.</param>
    /// <returns></returns>
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, Action<RabbitMQOptions> rabbitMqOptions)
    {
        services.Configure(rabbitMqOptions);
        services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
        services.AddSingleton<IHostedService, MessageQueueHostedService>();

        return services;
    }

    /// <summary>
    /// Adds the elastic search.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="elasticMqOptions">The elastic mq options.</param>
    /// <returns></returns>
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, Action<ElasticSearchOptions> elasticMqOptions)
    {
        services.Configure(elasticMqOptions);
        services.AddSingleton<IElasticSearchClientProvider, ElasticSearchClientProvider>();

        return services;
    }

    /// <summary>
    /// Registers the rabbit mq.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, ConfigurationManager configuration)
    {
        try
        {
            IConfigurationSection section = configuration.GetSection("RabbitMQ");
            if (section != null)
            {
                services.AddRabbitMQ(options =>
                {
                    options.UserName = section["UserName"];
                    options.Password = section["Password"];
                    options.Port = int.Parse(section["Port"] ?? "0");
                    options.HostName = section["HostName"];
                    options.Enable = bool.Parse(section["Enable"] ?? "false");
                    options.EndPoints = section.GetSection("EndPoints").GetChildren().Select(x => x.Value ?? string.Empty).ToList();
                });
            }
            else
            {
                services.AddRabbitMQ(options => { options.Enable = false; });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RabbitMQ Connection failed: {ex.Message}", ex);
        }

        return services;
    }

    /// <summary>
    /// Registers the elastic search.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterElasticSearch(this IServiceCollection services, ConfigurationManager configuration)
    {
        try
        {
            services.AddElasticSearch(options =>
            {
                IConfigurationSection section = configuration.GetSection("ElasticSearch");
                if (section != null)
                {
                    options.Url = section["Url"];
                    options.DefaultIndex = section["DefaultIndex"];
                    options.CertificateFingerprint = section["CertificateFingerprint"];
                    options.UserName = section["UserName"];
                    options.Password = section["Password"];
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Elastic Search Connection failed: {ex.Message}", ex);
        }

        return services;
    }

    /// <summary>
    /// Registers the redis.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterRedis(this IServiceCollection services, ConfigurationManager configuration)
    {
        try
        {
            services.AddRedis(options =>
            {
                IConfigurationSection section = configuration.GetSection("Redis");
                if (section != null)
                {
                    options.UserName = section["UserName"];
                    options.Password = section["Password"];
                    options.ConnectionString = section["ConnectionString"];
                    options.Port = int.Parse(section["Port"] ?? string.Empty);
                    options.EndPoints = section.GetSection("EndPoints").GetChildren().Select(x => x.Value ?? string.Empty).ToList();
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB Connection failed: {ex?.Message}", ex);
            services.AddHealthChecks().AddCheck<RedisConnectionHealthCheck>("Redis");
        }

        return services;
    }

    /// <summary>
    /// Adds the service collection.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="authentication">if set to <c>true</c> [authentication].</param>
    /// <returns></returns>
    public static IServiceCollection AddServiceCollection<TContext>(this WebApplicationBuilder builder, ProgramConfigure programConfigure) where TContext : DbContext, IBasicDbContext
    {
        IServiceCollection services = builder.Services;
        ConfigurationManager configuration = builder.Configuration;

        //Environment Name
        string env = builder.Environment.EnvironmentName;

        //Json File
        configuration.AddAllJsonFiles(env);

        //Swagger
        services.AddEndpointsApiExplorer().AddSwagger();

        //Authentication and Authorization
        if (programConfigure.EnableAuthentication)
        {
            services.AddKeyCloak(configuration);
        }

        services.AddControllers();
        services.AddHttpClient();
        services.AddCurrentContext();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddScoped<IServiceInvoker, ServiceInvoker.ServiceInvoker>();
        services.AddScoped<ITypeAdapter, AutomapperTypeAdapter>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<RestClient>();
        services.AddTransient<IHkshClient, HkshClient>();
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly());

        //Add Nlog
        services.AddNLog(configuration);

        //Inject Iservice
        services.InjectIService();

        //Variable Options
        services.AddVariableOptions(configuration);

        //Minio Options
        services.AddMinioOptions(configuration);

        //Enable AuditLog Options
        services.AddEnableAuditLogOptions(configuration);

        //RabbitMQ
        if (programConfigure.EnableRabbitMQ)
        {
            services.RegisterRabbitMQ(configuration);
        }

        //Redis
        services.RegisterRedis(configuration);

        //Elastic
        services.RegisterElasticSearch(configuration);

        //Kafka
        if (programConfigure.EnableKafka)
        {
            services.AddCap(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("SqlServer") ?? string.Empty);
                x.UseKafka(configuration.GetConnectionString("Kafka") ?? string.Empty);
            });
        }

        //File
        services.Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.SECTION));
        services.AddTransient<FileUploadOptions>();

        //Enable Cors
        if (programConfigure.EnableCors)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", policy =>
                {
                    policy.WithOrigins("*")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        //DbContext
        if (programConfigure.EnableDbContext)
        {
            services.RegisterDbContextRelated<TContext>(configuration);
        }

        //Api Version
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(ApiVersionConstant.VERSION_ONE, ApiVersionConstant.VERSION_ZERO);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        return services;
    }

    /// <summary>
    /// Adds the service collection.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="programConfigure">The program configure.</param>
    /// <returns></returns>
    public static IServiceCollection AddServiceCollection(this WebApplicationBuilder builder, ProgramConfigure programConfigure)
    {
        IServiceCollection services = builder.Services;
        ConfigurationManager configuration = builder.Configuration;

        //Environment Name
        string env = builder.Environment.EnvironmentName;

        //Json File
        configuration.AddAllJsonFiles(env);

        //Swagger
        services.AddEndpointsApiExplorer().AddSwagger();

        //Authentication and Authorization
        if (programConfigure.EnableAuthentication)
        {
            services.AddKeyCloak(configuration);
        }

        services.AddControllers();
        services.AddHttpClient();
        services.AddCurrentContext();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddScoped<IServiceInvoker, ServiceInvoker.ServiceInvoker>();
        services.AddScoped<ITypeAdapter, AutomapperTypeAdapter>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<RestClient>();
        services.AddTransient<IHkshClient, HkshClient>();
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly());

        //Add Nlog
        services.AddNLog(configuration);

        //Inject Iservice
        services.InjectIService();

        //Variable Options
        services.AddVariableOptions(configuration);

        //Minio Options
        services.AddMinioOptions(configuration);

        //Enable AuditLog Options
        services.AddEnableAuditLogOptions(configuration);

        //RabbitMQ
        if (programConfigure.EnableRabbitMQ)
        {
            services.RegisterRabbitMQ(configuration);
        }

        //Redis
        services.RegisterRedis(configuration);

        //Elastic
        services.RegisterElasticSearch(configuration);

        //Kafka
        if (programConfigure.EnableKafka)
        {
            services.AddCap(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("SqlServer") ?? string.Empty);
                x.UseKafka(configuration.GetConnectionString("Kafka") ?? string.Empty);
            });
        }

        //File
        services.Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.SECTION));
        services.AddTransient<FileUploadOptions>();

        //Enable Cors
        if (programConfigure.EnableCors)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", policy =>
                {
                    policy.WithOrigins("*")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        //Api Version
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(ApiVersionConstant.VERSION_ONE, ApiVersionConstant.VERSION_ZERO);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        return services;
    }
}