using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoreGuide.APIDotNet.Controllers
{
    public class TestController : ApiController
    {

        static HttpClient _client = new HttpClient();

        #region Bad - dead lock
        [HttpGet]
        public IHttpActionResult Bad()
        {
            var message = BadGet().GetAwaiter().GetResult();
            return Ok(message);
        }

        async Task<string> BadGet()
        {
            var result = await _client
                .GetStringAsync("https://www.google.com");
            return result;
        }
        #endregion

        #region Good - no dead lock
        [HttpGet]
        public IHttpActionResult Good()
        {
            var message = GoodGet().GetAwaiter().GetResult();
            return Ok(message);
        }

        async Task<string> GoodGet()
        {
            var result = await _client
                .GetStringAsync("https://www.google.com")
                .ConfigureAwait(false);
            return result;
        }
        #endregion

        [HttpGet]
        public IHttpActionResult CurrentThread()
        {
            var thread = AsyncMethodLong().GetAwaiter().GetResult();
            return Ok(thread);
        }

        private async Task<string> AsyncMethodLong()
        {
            var thread = $"Thread id in the method is: {Thread.CurrentThread.ManagedThreadId}";
            await Task.Delay(3000).ConfigureAwait(false);
            return thread;
        }
    }
}
