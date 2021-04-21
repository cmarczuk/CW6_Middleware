using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CW6_Middleware.Middleware
{
    public class CustomMiddleware
    {
        public readonly RequestDelegate next;

        public CustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IBrowserDetector detector)
        {
            var browser = detector.Browser;

            if (browser.Name == BrowserNames.Edge || browser.Name == BrowserNames.EdgeChromium || browser.Name == BrowserNames.InternetExplorer)
            {
                await httpContext.Response.WriteAsync("<html lang='pl'><head><meta charset='utf-8'></head><body><h1>Przeglądarka nie jest obsługiwana</h1></body></html>");
            }
            else
            {
                await this.next.Invoke(httpContext);
            }
        }
    }
}