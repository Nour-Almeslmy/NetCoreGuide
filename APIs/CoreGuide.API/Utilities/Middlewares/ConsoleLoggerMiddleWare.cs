using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CoreGuide.API.Utilities.Middlewares
{
    public class ConsoleLoggerMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Before next middleware");
            await next(context);
            Console.WriteLine("After next middleware");
        }
    }
}
