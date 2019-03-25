using System;
using System.Text;
using System.Threading.Tasks;
using Aigang.Contracts.Executor.Utils;
using Microsoft.AspNetCore.Http;

namespace Aigang.Contracts.Executor.API
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (context.Request.Path.HasValue &&
                context.Request.Path.Value.Contains("/status") || context.Request.Path.Value.Contains("/swagger"))
            {
                await _next.Invoke(context);
            }
            else if (authHeader != null)
            {
                try
                {
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var apiSecret = encoding.GetString(Convert.FromBase64String(authHeader));
                    var apiKey = ConfigurationManager.GetString("apiKey");

                    if (apiSecret.Equals(apiKey))
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 401; //Unauthorized
                        return;
                    }
                }
                catch
                {
                    context.Response.StatusCode = 401; //Unauthorized
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }
        }
    }
}