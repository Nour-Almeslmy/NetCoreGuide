using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        static HttpClient _client = new HttpClient();

        #region Bad in dot net but good here
        [HttpGet("BadDotNet")]
        public IActionResult BadDotNet()
        {
            var result = BadExample();
            return Ok(result);
        }

        string BadExample()
        {
            var message = BadGet().GetAwaiter().GetResult();
            return message;
        }

        async Task<string> BadGet()
        {
            var result = await _client.GetStringAsync("https://www.google.com");
            return result;
        }
        #endregion

        #region Return task instead of void
        [HttpGet("ReturnVoid")]
        public IActionResult ReturnVoid_NotCathcingError()
        {
            try
            {
                ReturnVoid_Bad();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        async void ReturnVoid_Bad()
        {
            await Task.Delay(1000);
            throw new NullReferenceException("Return void exception");
        }

        [HttpGet("ReturnTask")]
        public async Task<IActionResult> ReturnVoid_CathcingError()
        {
            try
            {
                await ReturnVoid_Good();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        async Task ReturnVoid_Good()
        {
            await Task.Delay(1000);
            throw new NullReferenceException("Return Task exception");
        }
        #endregion

        #region Wating task in block

        [HttpGet("BlockWithNoAwait")]
        public Task BlockWithNoAwait_NotCatchingError()
        {
            try
            {
                return ReturnVoid_Good();
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        [HttpGet("BlockWithAwait")]
        public async Task BlockWithAwait_CatchingError()
        {
            try
            {
                await ReturnVoid_Good();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Aggregate exception
        [HttpGet("AggregateException")]
        public Task AggregateException()
        {
            try
            {
                ReturnVoid_Good().Wait();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(ex.Message);
            }
        }

        [HttpGet("DetailedException")]
        public Task NotAggregateException()
        {
            try
            {
                ReturnVoid_Good().GetAwaiter().GetResult();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(ex.Message);
            }
        }
        #endregion

    }
}
