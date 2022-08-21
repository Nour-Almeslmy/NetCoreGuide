using CoreGuide.BLL.Models.ConfigurationSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly AllowedFileSettings _options;
        private readonly AllowedFileSettings _optionsSnapshot;
        private readonly ILogger<TestsController> _logger;

        public TestsController(IOptionsSnapshot<AllowedFileSettings> optionsSnapshot, IOptions<AllowedFileSettings> options, ILogger<TestsController> logger)
        {
            _options = options.Value;
            _optionsSnapshot = optionsSnapshot.Value;
            _logger = logger;
        }

        [HttpGet("sync")]
        public IActionResult Sync()
        {
            WaitSync("sync");
            return Ok("sync");
        }

        private void WaitSync(string mode)
        {
            var dateToWaitFor = DateTime.Now.AddSeconds(15);
            var currentDate = DateTime.Now;
            var secondsRemaining = 0;
            while (currentDate < dateToWaitFor)
            {
                var ms = dateToWaitFor - currentDate;
                if (secondsRemaining != ms.Seconds)
                {
                    secondsRemaining = ms.Seconds;
                    Console.WriteLine($"{ms.Seconds} seconds remaining");
                }
                currentDate = DateTime.Now;

            }
            Console.WriteLine($"Waiting {mode}");
        }

        [HttpGet("async")]
        public async Task<IActionResult> Async()
        {
            var task = Task.Run(() => WaitSync("async"));
            await task;
            return Ok("async");
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> cancel(CancellationToken cancellationToken)
        {
            try
            {
                var task = Task.Delay(5000, cancellationToken);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await task;
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("done");
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine(cancellationToken.IsCancellationRequested);
                Console.Error.WriteLine(ex.ToString());
            }
            return Ok();
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            // No syncronization context
            var number = GetRandomNumber().GetAwaiter().GetResult();
            return Ok(number);
        }

        private async Task<int> GetRandomNumber()
        {
            return await Task.Run(() =>
            {
                return 2;
            }
            );
        }

        [HttpGet("thread1")]
        public async Task<IActionResult> WaitWithNoOtherWork()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Thread id in the endpoint before execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Work before");
            int number = await AsyncMethod();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("after work");
            Console.WriteLine($"Thread id in the endpoint after execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = ConsoleColor.White;
            return Ok(number);
        }

        [HttpGet("thread2")]
        public async Task<IActionResult> WaitWithNoOtherWorkOnAnotherStep()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Thread id in the endpoint before execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Work before");
            var numberTask = AsyncMethod();
            var number = await numberTask;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("after work");
            Console.WriteLine($"Thread id in the endpoint after execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = ConsoleColor.White;
            return Ok(number);
        }

        [HttpGet("thread3")]
        public async Task<IActionResult> WaitWithOtherWorkDoneWhileAwaitedFinished()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Thread id in the endpoint before execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Work before");
            var numberTask = AsyncMethod();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("some work after");
            var number = await numberTask;
            Console.WriteLine($"Thread id in the endpoint after execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = ConsoleColor.White;
            return Ok(number);
        }

        [HttpGet("thread4")]
        public async Task<IActionResult> WaitWithOtherWorkDoneWhileAwaitedNotFinished()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Thread id in the endpoint before execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Work before");
            var numberTask = AsyncMethodLong();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("some work after");
            await numberTask;
            Console.WriteLine($"Thread id in the endpoint after execution is: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = ConsoleColor.White;
            return Ok();
        }

        private static async Task<int> AsyncMethod()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Thread id in the method is: {Thread.CurrentThread.ManagedThreadId}");
            return await Task.Run(() =>
            {
                return 2;
            });
        }
        private static Task AsyncMethodLong()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Thread id in the method is: {Thread.CurrentThread.ManagedThreadId}");
            return Task.Delay(3000);
        }
        [HttpGet("config")]
        public IActionResult Config()
        {
            var result = $"IOptions: {_options.MaximumImageSize}\r\nIOptionsSnapshot: {_optionsSnapshot.MaximumImageSize}";
            return Ok(result);
        }

        [HttpGet("log")]
        public IActionResult Log()
        {
            _logger.LogTrace("Trace msg\r\n");
            _logger.LogDebug("Debug msg\r\n");
            _logger.LogInformation("Information msg\r\n");
            _logger.LogWarning("Warning msg\r\n");
            return Ok();
        }

        [HttpGet("logex")]
        public IActionResult Logex()
        {
            try
            {

                throw new AggregateException("Errooooooooooooooooooooooooooooooooooooooor",
                    new ArgumentNullException("first inner excpetion",
                    new ArgumentException("second exception")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

    }
}
