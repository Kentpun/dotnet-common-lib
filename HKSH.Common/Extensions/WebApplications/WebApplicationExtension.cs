using Elastic.Apm.NetCoreAll;
using HKSH.Common.Base;
using HKSH.Common.File;
using HKSH.Common.ShareModel;
using HKSH.Common.XxlJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HKSH.Common.Extensions.WebApplications
{
    public static class WebApplicationExtension
    {
        /// <summary>
        /// Uses the customize static files.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static WebApplication UseCustomizeStaticFiles(this WebApplication app, ConfigurationManager configuration)
        {
            var services = new ServiceCollection().AddOptions().Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.Section)).BuildServiceProvider();

            var fileOptions = services.GetService<IOptions<FileUploadOptions>>();

            if (fileOptions == null)
            {
                return app;
            }

            if (fileOptions.Value.Directory != null && fileOptions.Value.Directory.Any())
            {
                foreach (var item in fileOptions.Value.Directory)
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), item.Name)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), item.Name));
                    }

                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), item.Name)),
                        RequestPath = item.RequestPath,
                    });
                }
            }

            return app;
        }

        /// <summary>
        /// Automatics the migration.
        /// </summary>
        /// <param name="app">The application.</param>
        public static WebApplication AutoMigration<TContext>(this WebApplication app) where TContext : DbContext, IBasicDbContext
        {
            using var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<TContext>();
            if (context != null)
            {
                var pendingMigrations = context?.Database?.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    context?.Database?.Migrate();
                }
            }
            return app;
        }

        /// <summary>
        /// Adds the web application.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="programConfigure">The program configure.</param>
        /// <returns></returns>
        public static WebApplication AddWebApplication<TContext>(this WebApplicationBuilder builder, ProgramConfigure programConfigure) where TContext : DbContext, IBasicDbContext
        {
            var configuration = builder.Configuration;
            var app = builder.Build();

            //Enable Cors
            if (programConfigure.EnableCors)
            {
                app.UseCors("Cors");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
            });

            app.UseRouting();
            app.MapControllers();

            //Authentication and Authorization
            if (programConfigure.EnableAuthentication)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            //Migration
            if (programConfigure.EnableMigration)
            {
                app.AutoMigration<TContext>();
            }

            //Apm
            if (programConfigure.EnableElasticApm)
            {
                app.UseAllElasticApm(configuration);
            }

            if (programConfigure.EnableBuffering)
            {
                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next(context);
                });
            }

            //XxlJob
            if (programConfigure.EnableXxlJob)
            {
                app.UseXxlJobExecutor();
            }

            //File
            if (programConfigure.EnableStaticFiles)
            {
                app.UseCustomizeStaticFiles(configuration);
            }

            return app;
        }

        /// <summary>
        /// Adds the web application.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="programConfigure">The program configure.</param>
        /// <returns></returns>
        public static WebApplication AddWebApplication(this WebApplicationBuilder builder, ProgramConfigure programConfigure)
        {
            var configuration = builder.Configuration;
            var app = builder.Build();

            if (programConfigure.EnableCors)
            {
                app.UseCors("Cors");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
            });

            app.UseRouting();
            app.MapControllers();

            //Authentication and Authorization
            if (programConfigure.EnableAuthentication)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            //Apm
            if (programConfigure.EnableElasticApm)
            {
                app.UseAllElasticApm(configuration);
            }

            if (programConfigure.EnableBuffering)
            {
                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next(context);
                });
            }

            //XxlJob
            if (programConfigure.EnableXxlJob)
            {
                app.UseXxlJobExecutor();
            }

            //File
            if (programConfigure.EnableStaticFiles)
            {
                app.UseCustomizeStaticFiles(configuration);
            }

            return app;
        }
    }
}