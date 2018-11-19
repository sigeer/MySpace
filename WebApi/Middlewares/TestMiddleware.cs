using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Utility;
using Utility.Exceptions;

namespace WebApi.Middlewares
{
    public class ApiErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(context.Response.StatusCode);
                if (ex is SigeerException)
                {
                    var myException = ex as SigeerException;
                    var jsonModel = new ResponseMessage("Request",Message.Error,myException.Message,myException.Params).ToString();
                    Console.WriteLine(jsonModel);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    await context.Response.WriteAsync(jsonModel);
                }
            }
            
        }
    }
        public static class ApiErrorMiddlewareExtensions
        {
            public static IApplicationBuilder UseApiError(
                this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<ApiErrorMiddleware>();
            }
        }
}