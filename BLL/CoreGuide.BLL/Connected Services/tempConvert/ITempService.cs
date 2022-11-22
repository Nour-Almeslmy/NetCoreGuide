using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Connected_Services.tempConvert
{
    public interface ITempService
    {
        Task<string> CelsiusToFahrenheitAsync(string Celsius);
        Task<string> FahrenheitToCelsiusAsync(string Fahrenheit);
    }
}
