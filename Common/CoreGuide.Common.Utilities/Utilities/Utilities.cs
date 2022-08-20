using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
//using Microsoft.AspNetCore.App.Ref;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreGuide.Common.Utilities.Utilities
{
    internal class Utilities : IUtilities
    {
        // to use you must add "services.AddHttpContextAccessor();"
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Utilities(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserAgent()
        {
            try
            {
                return _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            }
            catch (Exception exp)
            {
               // _logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }

        public string GetInternalServerIP()
        {
            try
            {
                var connectionFeature = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>();
                var ip = connectionFeature?.LocalIpAddress?.ToString();
                if (!string.IsNullOrWhiteSpace(ip)) return ip;
                var serverFeature = _httpContextAccessor.HttpContext.Features.Get<IServerVariablesFeature>();
                return serverFeature["LOCAL_ADDR"];
            }
            catch (Exception exp)
            {
                //_logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }

        public string GetUserIPAddress()
        {
            try
            {
                var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
                if (!string.IsNullOrWhiteSpace(ip)) return ip;
                var serverFeature = _httpContextAccessor.HttpContext.Features.Get<IServerVariablesFeature>();
                ip = serverFeature["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                    ip = serverFeature["REMOTE_ADDR"];

                if (ip.Contains(","))
                    ip = ip.Split(',')[0];
                return ip;
            }
            catch (Exception exp)
            {
                //_logger.LogError(exp.Message, exp, false);
                return string.Empty;
            }
        }

        public bool IsValidDial(string dial)
        {
            Regex regexDial = new Regex(Strings.Regex.OrangeDial, RegexOptions.Compiled);
            return regexDial.IsMatch(dial);
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateLanguageInput(string language)
        {
            if (string.IsNullOrEmpty(language))
                return false;
            if (language != Strings.Cultures.Ar && language != Strings.Cultures.En)
                return false;
            return true;
        }

        public string GetCurrentLanguage()
        {
            // to use you must add " services.AddLocalization();" and add middle ware "app.UseRequestLocalization"
            var cultureFeature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            return cultureFeature.RequestCulture.Culture.Name;
        }


        public string GetEnumDisplayName<T>(T action) where T : Enum
        {
            string name = action.ToString();
            var attribute = action.GetType().GetMember(action.ToString()).First().GetCustomAttribute<DisplayAttribute>();
            if (attribute != null)
            {
                name = attribute.Name;
            }
            return name;
        }
    }
}
