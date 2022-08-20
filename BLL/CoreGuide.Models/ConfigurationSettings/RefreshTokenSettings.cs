using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.ConfigurationSettings
{
    public class RefreshTokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationTimeInMinutes { get; set; }
    }
}
