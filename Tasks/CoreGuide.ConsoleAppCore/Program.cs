// See https://aka.ms/new-console-template for more information
using CoreGuide.ConsoleApp;

PrintCurrentThread();
Console.WriteLine("Hello, World!");
PrintCurrentThread();

var fileDataResponse = ConcurrentExamples.DownloadFile();
PrintCurrentThread();
var userNameResponse = ConcurrentExamples.GetUserName();
PrintCurrentThread();

var fileData = await fileDataResponse;
var userName = await userNameResponse;

Console.WriteLine($"The file data is :{fileData}");

var isValid = await ConcurrentExamples.ValidateUserName(userName);
PrintCurrentThread();
if (isValid)
    await ConcurrentExamples.PrintValidUserName(userName);

var weatherApi = await ConcurrentExamples.GetCurrentWeather();
PrintCurrentThread();
long count = 0;
var countResponse = Task.Run<long>(() =>
{
    for (long i = 0; i < 10_000_000_000; i++)
    {
        count++;
    }
    return count;
});
Console.WriteLine($"The current weather is :{weatherApi}");
var currentCount =  await countResponse;
PrintCurrentThread();
Console.WriteLine(currentCount);
Console.WriteLine("End of Application");




static void PrintCurrentThread()
{
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
}