using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreGuide.CoreAPIs
{
    public class ConsoleFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _input;

        public ConsoleFilterAttribute(string input)
        {
            _input = input;
        }
        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            Console.WriteLine("Filter ... Before");
            Console.WriteLine(_input);
            await next();
            Console.WriteLine("Filter ... After");
        }
    }
}
