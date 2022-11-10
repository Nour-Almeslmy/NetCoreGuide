using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class MiddleWares
    {
        #region Definition
        /// Middleware is software that's assembled into an app pipeline to handle requests and responses. Each component:
        /// Chooses whether to pass the request to the next component in the pipeline.
        /// Can perform work before and after the next component in the pipeline.

        /// Request delegates are used to build the request pipeline. The request delegates handle each HTTP request.
        #endregion

        #region Steps

        #region app.Run
        /// It recieves only httpContext. Doesn't know about next middleware
        /// It is used as terminal delegate as it ends the pipeline. so it is more of conevtion to use Run to indicate the end of pipeline
        const string runExample = "app.Run(async context => await context.Response.WriteAsync(\"Terminal delegate\"));";
        #endregion

        #region app.Use
        /// It knows httpContext and next delegate for next middlewares.
        /// It wraps the next middle ware.
        const string useExample = @"
                                     app.Use(async (context, next) =>
                                                {
                                                    Console.WriteLine(""Before"");
                                                    await next();
                                                    Console.WriteLine(""After"");
                                                });";

        #endregion

        #region app.UseWhen
        /// It knows httpContext and next delegate for next middlewares., if you use next delegate it will continue in the rest of pipe line
        /// It wraps the next middle ware.
        const string useWhenExample = "app.UseWhen(\"Map\", app => app.Run(async context => await context.Response.WriteAsync(\"map handler\")));";


        #endregion

        #region app.Map
        /// Used as terminal delegate for certain path, but you can configure in it multiple middlewares
        const string mapExample = "app.Map(\"Map\", app => app.Run(async context => await context.Response.WriteAsync(\"map handler\")));";
        #endregion

        #region app.MapWhen
        /// Used as terminal delegate for certain condition, but you can configure in it multiple middlewares, but it won't continue on the after middlewares in the configure method
        const string mapWhenExample = "app.MapWhen(\"Map\", app => app.Run(async context => await context.Response.WriteAsync(\"map handler\")));";
        #endregion

        #region Create custom middleware
        /// 1) Create class to implement IMiddleware interface
        /// 2) Write logic in it
        /// 3) Register the service in configure services
        const string registrationExample = "services.AddTransient<ConsoleLoggerMiddleWare>();";
        /// 4) Add it to piple line in Configure method
        const string pipeLineExample = "app.UseMiddleware<ConsoleLoggerMiddleWare>();";
        #endregion

        #endregion

        #region References
        /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
        /// https://www.youtube.com/watch?v=5eifH7LEnGo&list=PL59L9XrzUa-nqfCHIKazYMFRKapPNI4sP&index=2
        #endregion
    }
}
