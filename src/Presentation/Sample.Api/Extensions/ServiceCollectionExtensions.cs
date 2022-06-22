using Sample.Application;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Sample.Infrastructure;

namespace Sample.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddMediatR(typeof(Startup).Assembly, typeof(AssembelyRecognizer).Assembly);

            services.RegisterSampleDependecies(configuration);
            return services;
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Description = "Sample Public api.",
                    Title = "Sample API",
                    Version = "v1",
                });

                options.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.ResolveConflictingActions(descriptors => descriptors.First());

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "API Key Authentication",
                    In = ParameterLocation.Header,
                    Name = "Authorization"
                });
                var basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                };
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });

            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }

}
