using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Filters
    {
        /// Runs after selecting the action to execute
        /// It has its own filter pipleline

        #region Custom Filter
        /// 1) Create class to inherit from IAsyncActionFilter [For synchronous behavior , inherit from IActionFilter]
        /// 2) Implement Interface
        /// 3) You can specify attribute usage by using AttributeUsage Filter on filter class, traget may be method, class,..etc;
        const string globaleExample2 = @"[AttributeUsage(AttributeTargets.All)]";

        #region Global regisestration
        /// 1) For global regiseteration, add in ConfigureServices
        const string globalExample = @"services.AddControllers(options =>
            {
                /// Global registeration
                //options.Filters.Add(new ConsoleLogFilterAttribute());
            });";
        /// 2) If it has dependency, you need to register the filter as service filter in AddControllerOptions
        const string globalDependencyExample = @"services.AddControllers(options =>
            {
                /// Global registeration
                //options.Filters.AddService<SerilogFilterAttribute>();
            });";
        /// 3) Register the filter class itself
        const string globalDependencyFilterExample = @"services.AddTransient<SerilogFilterAttribute>();";
        #endregion

        #region Use as Attribute
        /// 1) Inherit Attribute class
        /// 2) If it has dependency, Register the filter class itself
        const string serviceDependencyFilterExample = @"services.AddTransient<SerilogFilterAttribute>();";
        /// 3) Use it as service filter
        const string serviceDependencyFilterUseExample = @"[ServiceFilter(typeof(SerilogFilterAttribute))]";
        /// 4) To pass parameters, use typeFilter
        const string serviceDependencyTypeFilterUseExample = "[TypeFilter(typeof(SerilogFilterAttribute),Arguments = new object[] {\"Action\"})]";
        #endregion


        #endregion

        #region Custom filter Order
        /// 1) Before Global
        /// 2) Before Controller
        /// 3) Before action
        /// 4) After action
        /// 5) After Controller
        /// 6) After global

        #region Reorder
        /// 1) Implement IOrderedFilter
        /// 2) Take the value in the CTOR
        /// 3) The less the value the higher order it takes
        #endregion

        #endregion

        #region Built in filter order
        /// 1) Authorization
        /// 2) Resources
        /// 3) Action
        /// 4) Exception
        /// 5) Result
        #endregion


        #region References
        /// https://www.youtube.com/watch?v=mKM6FbxMGI8&list=PL59L9XrzUa-nqfCHIKazYMFRKapPNI4sP&index=26
        /// https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-6.0
        /// https://www.youtube.com/watch?v=kqwjrJ4kb9Q&list=PL59L9XrzUa-nqfCHIKazYMFRKapPNI4sP&index=26
        /// https://www.dotnetnakama.com/blog/asp-dotnetcore-filters-avoid-duplicating-code/#filters-vs-middleware
        #endregion
    }
}
