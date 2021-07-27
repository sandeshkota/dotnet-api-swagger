using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api_whiteapp.Redoc
{
    public class RedocConfiguration
    {
        public static void UseRedoc(IApplicationBuilder app)
        {
            app.UseReDoc(options =>
            {
                options.RoutePrefix = "docs";
                //options.SpecUrl("/v1/swagger.json");
                options.EnableUntrustedSpec();
                options.ScrollYOffset(10);
                options.HideHostname();
                options.HideDownloadButton();
                options.ExpandResponses("200,201");
                options.RequiredPropsFirst();
                options.NoAutoAuth();
                options.PathInMiddlePanel();
                options.HideLoading();
                options.NativeScrollbars();
                //options.DisableSearch();
                options.OnlyRequiredInSamples();
                options.SortPropsAlphabetically();
            });
        }
    }
}
