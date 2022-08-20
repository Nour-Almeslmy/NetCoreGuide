namespace CoreGuide.ConsoleApp
{
    public static class ConcurrentExamples
    {
        public static async Task<string> DownloadFile()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Downloading File");
            Console.ForegroundColor = ConsoleColor.White;
            await Task.Delay(3000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Downloading Finished");
            Console.ForegroundColor = ConsoleColor.White;
            return "File data";
        }

        public static async Task<string> GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Connecting to DB");
            Console.ForegroundColor = ConsoleColor.White;
            await Task.Delay(2000);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Name fetched");
            Console.ForegroundColor = ConsoleColor.White;
            return "waleed";
        }

        public static Task<bool> ValidateUserName(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Validating property");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Valid");
            Console.ForegroundColor = ConsoleColor.White;
            return Task.FromResult(true);
        }

        public static Task PrintValidUserName(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{userName} is valid");
            Console.ForegroundColor = ConsoleColor.Cyan;
            return Task.CompletedTask;
        }
        public static async Task<string> GetCurrentWeather()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Calling API");
            Console.ForegroundColor = ConsoleColor.White;
            await Task.Delay(2000);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("API call finishied");
            Console.ForegroundColor = ConsoleColor.White;
            return "Cold";
        }

    }
}
