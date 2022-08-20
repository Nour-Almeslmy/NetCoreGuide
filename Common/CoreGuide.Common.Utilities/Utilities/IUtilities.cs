using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.Utilities.Utilities
{
    public interface IUtilities
    {
        string GetCurrentLanguage();
        string GetEnumDisplayName<T>(T action) where T : Enum;
        string GetInternalServerIP();
        string GetUserAgent();
        string GetUserIPAddress();
        bool IsValidDial(string dial);
        bool IsValidEmail(string email);
        bool ValidateLanguageInput(string language);
    }
}
