using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CoreGuide.API.Utilities.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class ConsoleLogFilterAttribute : Attribute, IAsyncActionFilter
    {
       

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Before ==> {context.ActionDescriptor.DisplayName}");
            Console.ForegroundColor = ConsoleColor.White;
            await next();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"After ==> {context.ActionDescriptor.DisplayName}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
