using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace gameapi2.Middlewares
{
    public class MySettings
    {
        public string apikey { get; set; }
    }
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MySettings _mySettings;

        public AuthenticationMiddleware(RequestDelegate next, IOptions<MySettings> mysettings)
        {
            _next = next;
            _mySettings = mysettings.Value;
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("x-api-key"))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Authentication Key is missing!");
                return;
            }
            else if (_mySettings.apikey != context.Request.Headers["x-api-key"])
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Authentication Key");
                return;
                
            }
            
            await _next.Invoke(context);

        }
    }
}