﻿using Elastic.Apm.NetCoreAll;
using HKSH.Common.File;
using HKSH.Common.Middlewares;
using HKSH.Common.ShareModel;
using HKSH.Common.ShareModel.Base;
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
    /// <summary>
    /// WebApplication Extension
    /// </summary>
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
            ServiceProvider services = new ServiceCollection().AddOptions().Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.SECTION)).BuildServiceProvider();

            IOptions<FileUploadOptions>? fileOptions = services.GetService<IOptions<FileUploadOptions>>();

            if (fileOptions == null)
            {
                return app;
            }

            if (fileOptions.Value.Directory != null && fileOptions.Value.Directory.Any())
            {
                foreach (FileDirectoryOptions item in fileOptions.Value.Directory)
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
            try
            {
                using IServiceScope scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
                TContext? context = scope.ServiceProvider.GetService<TContext>();
                if (context != null && context.Database != null && context.Database.CanConnect())
                {
                    IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        context.Database?.Migrate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EF Migration failed: {ex.Message}", ex);
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
            ConfigurationManager configuration = builder.Configuration;
            WebApplication app = builder.Build();

            app.UseMiddleware<ExceptionHandleMiddleware>();

            //Enable Cors
            app.UseCors("Cors");

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
                try
                {
                    _ = app.UseAllElasticApm(configuration);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Elastic Apm Connection failed: {ex.Message}", ex);
                }
            }

            if (programConfigure.EnableBuffering)
            {
                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next(context);
                });
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
            ConfigurationManager configuration = builder.Configuration;
            WebApplication app = builder.Build();

            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseCors("Cors");

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
                try
                {
                    _ = app.UseAllElasticApm(configuration);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Elastic Apm Connection failed: {ex.Message}", ex);
                }
            }

            if (programConfigure.EnableBuffering)
            {
                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next(context);
                });
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