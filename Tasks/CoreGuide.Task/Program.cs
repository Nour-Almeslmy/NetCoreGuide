// See https://aka.ms/new-console-template for more information
using CoreGuide.ConsoleApp;

Console.WriteLine("Hello, World!");

var fileDataResponse = ConcurrentExamples.DownloadFile();
var userNameResponse = ConcurrentExamples.GetUserName();

var fileData = await fileDataResponse;
var userName = await userNameResponse;

Console.WriteLine($"The file data is :{fileData}");

var isValid = await ConcurrentExamples.ValidateUserName(userName);
if (isValid)
    await ConcurrentExamples.PrintValidUserName(userName);

var weatherApi = await ConcurrentExamples.GetCurrentWeather();
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
Console.WriteLine(currentCount);
Console.WriteLine("End of Application");