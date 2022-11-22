using CoreGuide.BLL.Models.ConfigurationSettings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using tempConvert;

namespace CoreGuide.BLL.Business.Connected_Services.tempConvert
{
    internal class TempService : ITempService
    {
        private readonly TempConvertSoapClient _tempConvertSoapClient;
        public TempService(IOptionsSnapshot<TempConvertServiceSettings> settings)
        {
            _tempConvertSoapClient = new TempConvertSoapClient(TempConvertSoapClient.EndpointConfiguration.TempConvertSoap, settings.Value.URL);
        }

        public Task<string> FahrenheitToCelsiusAsync(string Fahrenheit)
        {
            return _tempConvertSoapClient.FahrenheitToCelsiusAsync(Fahrenheit);
        }

        public Task<string> CelsiusToFahrenheitAsync(string Celsius)
        {
            return _tempConvertSoapClient.CelsiusToFahrenheitAsync(Celsius);
        }
    }
}
