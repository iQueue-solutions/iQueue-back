using Microsoft.OpenApi.Models;

namespace IQueueAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "IQueue API",
                Version = "v1",
                Description = "IQueue API by IQueueSolutions. Developed as university team project.",
                Contact = new OpenApiContact
                {
                    Name = "Denys Spektrov", 
                    Email = "denys.spektrov@nure.ua",
                }
            });

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                { 
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            },
                            Name = "Bearer", 
                        }, 
                        new List<string>() 
                    } 
                });
        });
    }
}