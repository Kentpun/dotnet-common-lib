using HKSH.Common.AuditLogs;
using HKSH.Common.AutoMapper;
using HKSH.Common.Base;
using HKSH.Common.Caching.Redis;
using HKSH.Common.File;
using HKSH.Common.RabbitMQ;
using HKSH.Common.Repository;
using HKSH.Common.ServiceInvoker;
using HKSH.Common.ShareModel;
using HKSH.Common.XxlJob;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using System.Reflection;

namespace HKSH.Common.Extensions.ServiceCollections;

/// <summary>
/// ServiceCollectionExtensions
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
            var securityScheme = new OpenApiSecurityScheme
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

            var allDocPaths = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var item in allDocPaths)
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
        IConfigurationSection nLogConnetctSection = configuration.GetSection("NLog:targets:db:connectionString");
        nLogConnetctSection.Value = configuration.GetConnectionString("SqlServer");
        LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));

        services.AddLogging(configure =>
        {
            configure.AddNLog(LogManager.Configuration);
        });

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
        //add default database
        services.AddDbContextPool<TContext>(option => option.UseSqlServer(configuration.GetConnectionString("SqlServer")), poolSize: 64);

        services.AddScoped<TContext, TContext>();
        services.AddDatabase<TContext>();

        return services;
    }

    /// <summary>
    /// Injects the i service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection InjectIService(this IServiceCollection services)
    {
        var types = Assembly.GetEntryAssembly()?.DefinedTypes.Where(a => typeof(IService).IsAssignableFrom(a));
        if (types != null)
        {
            foreach (var type in types)
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
        var executingAssembly = Assembly.GetExecutingAssembly();
        var jsonConfigs = executingAssembly.GetManifestResourceNames().Where(a => a.EndsWith(".json"));
        foreach (var json in jsonConfigs)
        {
            var stream = executingAssembly.GetManifestResourceStream(json);
            if (stream != null && stream.Length > 0)
            {
                configuration.AddJsonStream(stream);
            }
        }

        var entryAssembly = Assembly.GetEntryAssembly();
        var entryJsonConfigs = entryAssembly?.GetManifestResourceNames().Where(a => a.EndsWith(".json"));
        if (entryJsonConfigs != null && entryJsonConfigs.Any())
        {
            foreach (var json in entryJsonConfigs)
            {
                var stream = entryAssembly?.GetManifestResourceStream(json);
                if (stream != null && stream.Length > 0)
                {
                    configuration.AddJsonStream(stream);
                }
            }
        }

        configuration.AddJsonFile("./Configmap/appsettings.configmap.common.json", true, true);
        configuration.AddJsonFile("./Secret/appsettings.secret.common.json", true, true);
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
        var authenticationOptions = configuration.GetSection(KeycloakAuthenticationOptions.Section).Get<KeycloakAuthenticationOptions>();
        services.AddKeycloakAuthentication(authenticationOptions);

        var authorizationOptions = configuration.GetSection(KeycloakProtectionClientOptions.Section).Get<KeycloakProtectionClientOptions>();
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
        services.Configure<List<VariableOptions>>(configuration.GetSection(VariableOptions.Section));
        services.AddTransient<List<VariableOptions>>();
        return services;
    }

    public static IServiceCollection AddEnableAuditLogOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<EnableAuditLogOptions>(configuration.GetSection(EnableAuditLogOptions.Section));
        services.AddTransient<EnableAuditLogOptions>();

        return services;
    }

    /// <summary>
    /// Adds the audit log store mongo database settings.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddAuditLogStoreMongoDbSettings(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<AuditLogStoreDatabaseSettings>(configuration.GetSection(AuditLogStoreDatabaseSettings.Section));
       
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
    /// Registers the rabbit mq.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddRabbitMQ(options =>
        {
            var section = configuration.GetSection("RabbitMQ");
            if (section != null)
            {
                options.UserName = section["UserName"];
                options.Password = section["Password"];
                options.Port = int.Parse(section["Port"] ?? string.Empty);
                options.HostName = section["HostName"];
                options.Enable = bool.Parse(section["Enable"] ?? "0");
                options.EndPoints = section.GetSection("EndPoints").GetChildren().Select(x => x.Value ?? string.Empty).ToList();
            }
        });

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
        services.AddRedis(options =>
        {
            var section = configuration.GetSection("Redis");
            if (section != null)
            {
                options.UserName = section["UserName"];
                options.Password = section["Password"];
                options.ConnectionString = section["ConnectionString"];
                options.Port = int.Parse(section["Port"] ?? string.Empty);
                options.EndPoints = section.GetSection("EndPoints").GetChildren().Select(x => x.Value ?? string.Empty).ToList();
            }
        });

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
        var services = builder.Services;
        var configuration = builder.Configuration;

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
        services.AddTransient<IServiceInvoker, ServiceInvoker.ServiceInvoker>();
        services.AddScoped<ITypeAdapter, AutomapperTypeAdapter>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly());

        //Add Nlog
        services.AddNLog(configuration);

        //Inject Iservice
        services.InjectIService();

        //Variable Options
        services.AddVariableOptions(configuration);

        //Enable AuditLog Options
        if (programConfigure.EnableEnableAuditLog)
        {
            services.AddEnableAuditLogOptions(configuration);
        }

        //Mongo DB
        if (programConfigure.EnableMongoDB)
        {
            services.AddAuditLogStoreMongoDbSettings(configuration);
        }

        //RabbitMQ
        if (programConfigure.EnableRabbitMQ)
        {
            services.RegisterRabbitMQ(configuration);
        }

        //Redis
        if (programConfigure.EnableRedis)
        {
            services.RegisterRedis(configuration);
        }

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
        if (programConfigure.EnableStaticFiles)
        {
            services.Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.Section));
            services.AddTransient<FileUploadOptions>();
        }

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

        //XxlJob
        if (programConfigure.EnableXxlJob)
        {
            services.AddxxlJobs(configuration);
        }

        return services;
    }

    public static IServiceCollection AddServiceCollection(this WebApplicationBuilder builder, ProgramConfigure programConfigure)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

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
        services.AddTransient<IServiceInvoker, ServiceInvoker.ServiceInvoker>();
        services.AddScoped<ITypeAdapter, AutomapperTypeAdapter>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly());

        //Add Nlog
        services.AddNLog(configuration);

        //Inject Iservice
        services.InjectIService();

        //Variable Options
        services.AddVariableOptions(configuration);

        //Enable AuditLog Options
        if (programConfigure.EnableEnableAuditLog)
        {
            services.AddEnableAuditLogOptions(configuration);
        }

        //Mongo DB
        if (programConfigure.EnableMongoDB)
        {
            services.AddAuditLogStoreMongoDbSettings(configuration);
        }

        //RabbitMQ
        if (programConfigure.EnableRabbitMQ)
        {
            services.RegisterRabbitMQ(configuration);
        }

        //Redis
        if (programConfigure.EnableRedis)
        {
            services.RegisterRedis(configuration);
        }

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
        if (programConfigure.EnableStaticFiles)
        {
            services.Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.Section));
            services.AddTransient<FileUploadOptions>();
        }

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

        //XxlJob
        if (programConfigure.EnableXxlJob)
        {
            services.AddxxlJobs(configuration);
        }

        return services;
    }
}