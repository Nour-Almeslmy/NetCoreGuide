using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class httpclient
    {

        #region Steps
        #region Basic usage with IHttpClient factory
        /// 1) Register service using httpClient " services.AddHttpClient();"
        /// 2) Use "IHttpClientFactory" to create clients
        ///  IHttpClientFactory pools the HttpMessageHandler instances created by the factory to reduce resource consumption. 
        ///  An HttpMessageHandler instance may be reused from the pool when creating a new HttpClient instance if its lifetime hasn't expired. so it avoid socket exhaution
        ///  ex in class OnDiskFileUploaderService
        #endregion

        #region Named clients
        /// 1) Configure client for certain service with a given name
        const string namedClientConfigurations = @"builder.Services.AddHttpClient(""GitHub"", httpClient =>
                                                    {
                                                        httpClient.BaseAddress = new Uri(""https://api.github.com/"");
                                                        // using Microsoft.Net.Http.Headers;
                                                        // The GitHub API requires two headers.
                                                        httpClient.DefaultRequestHeaders.Add(
                                                            HeaderNames.Accept, ""application/vnd.github.v3+json"");
                                                            httpClient.DefaultRequestHeaders.Add(
                                                                HeaderNames.UserAgent, ""HttpRequestsSample"");
                                                    });";
        /// 2) using httpclientFactory, create the client using the given name, ex "var httpClient = _httpClientFactory.CreateClient("GitHub");"
        #endregion

        #region Typed clients
        /// 1) Inject the class with httpClient as parameter in CTOR
        /// 2) register the class as using addhttpclient, ex: "services.AddHttpClient<IGitHubService,GitHubService>();", this registration is transient, but the instance of httpClient and service is created with factory method
        /// It can have details as well
        const string typedClientConfiguration = @"builder.Services.AddHttpClient<IGitHubService,GitHubService>(httpClient =>
                                                    {
                                                        httpClient.BaseAddress = new Uri(""https://api.github.com/"");

                                                        // ...
                                                    });";
        #endregion
        #endregion


        #region References
        /// https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0
        #endregion
    }
}
