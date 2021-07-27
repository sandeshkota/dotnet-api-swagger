using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_api_swagger.Swagger
{
    public static class SwaggerConfiguration
    {
        #region Swagger Generator Configuration
        public static void AddSwagger(IServiceCollection services)
        {
            var dict = new Dictionary<string, SwaggerAuthorDetail>();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        name: "v1",
                        info: new OpenApiInfo
                        {
                            Title = "Weather Forecast Application",
                            Version = "v1",
                            Description = "Sample Weather Forecast application",
                            License = new OpenApiLicense
                            {
                                Url = new Uri("https://github.com/sandeshkota/dotnet-api-whiteapp/blob/main/assets/license.md"),
                                Name = "Sample License"
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

                    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API - V2", Version = "v2" });

                    ConfigureSwaggerAuth(options);
                    ConfigureSwaggerAdditionalSettings(options);

                    options.IncludeXmlComments(GetFilesPath());
                }
            );
        }

        private static void ConfigureSwaggerAuth(SwaggerGenOptions options)
        {
            // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("/auth-server/connect/authorize", UriKind.Relative),
                        Scopes = new Dictionary<string, string>
                        {
                            { "readAccess", "Access read operations" },
                            { "writeAccess", "Access write operations" }
                        }
                    }
                }
            });
        }

        private static void ConfigureSwaggerAdditionalSettings(SwaggerGenOptions options) 
        {
            options.EnableAnnotations();

            //// Ignore [Obsolete] methods and properties
            //options.IgnoreObsoleteActions();
            //options.IgnoreObsoleteProperties();

            //// Grouping Swagger Methods by HTTP Type
            //options.TagActionsBy(api => api.HttpMethod);

            //// If two model has same name, use model full name (add namespace details too)
            //options.CustomSchemaIds((type) => type.FullName);
        }

        private static string GetFilesPath()
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            return xmlPath;
        }
        #endregion


        #region Swagger UI Configuration
        public static void UseSwagger(IApplicationBuilder app)
        {
            // To expose swagger json
            app.UseSwagger();

            // To use swagger json and build the UI
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "My Swagger UI";
                
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");

                options.InjectStylesheet("/swagger-ui/custom.css");

                ConfigureSwaggerUIAuth(options);
                ConfigureSwaggerUIAdditionalSettings(options);
            });
        }

        private static void ConfigureSwaggerUIAuth(SwaggerUIOptions options)
        {
            options.OAuthClientId("test-id");
            options.OAuthClientSecret("test-secret");
            options.OAuthRealm("test-realm");
            options.OAuthAppName("test-app");
            options.OAuthScopeSeparator(", ");
            options.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "source", "swagger" } });
            options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        }

        private static void ConfigureSwaggerUIAdditionalSettings(SwaggerUIOptions options)
        {
            //// To customize the URL //localhost:3592/docs/index.html
            //options.RoutePrefix = "docs";

            // set to -1 to hide models
            options.DefaultModelExpandDepth(2);
            
            // Shows Unique Operation ID at the right side of each operation
            options.DisplayOperationId();
            
            // Shows the response time after trying out any operation
            options.DisplayRequestDuration();

            // To Expand/Collapse the Operations
            options.DocExpansion(DocExpansion.None);
            
            // Allows to filter by Tags
            options.EnableFilter();

            options.DefaultModelRendering(ModelRendering.Model);

            //options.EnableDeepLinking();
            //options.MaxDisplayedTags(5);
            //options.ShowExtensions();
            //options.ShowCommonExtensions();
            //options.EnableValidator();
            //options.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);

            options.UseRequestInterceptor("(req) => { req.headers['x-my-custom-header'] = 'MyCustomValue'; return req; }");
            options.UseResponseInterceptor("(res) => { console.log('Custom interceptor intercepted response from:', res.url); return res; }");
        }
        #endregion

    }
}
