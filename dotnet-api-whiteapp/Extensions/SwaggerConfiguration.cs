using System;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.IO;

namespace dotnet_api_whiteapp.Extensions
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        name: "v1",
                        info: new OpenApiInfo
                        {
                            Title ="Weather Forecast Application",
                            Version = "v1",
                            Description = "Sample Weather Forecast application",
                            License = new OpenApiLicense 
                            { 
                                Url= new Uri("https://github.com/sandeshkota/dotnet-api-whiteapp/blob/main/assets/license.md"), 
                                Name ="Sample License" 
                            },
                            TermsOfService = new Uri("https://github.com/sandeshkota/dotnet-api-whiteapp/blob/main/assets/termsofservice.md"),
                            Contact = new OpenApiContact
                            {
                                Name = "Sandesh Kota",
                                Email = "sandeshjkota@gmail.com",
                                Url = new Uri("https://sandeshkota.github.io")
                            }
                        }
                    );

                    options.IncludeXmlComments(GetFilesPath());
                }
            );
        }

        private static string GetFilesPath()
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            return xmlPath;
        }

    }
}
