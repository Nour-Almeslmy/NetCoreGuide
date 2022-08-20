using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.ConfigurationSettings
{
    public class IdentitySettings
    {
        public PassowrdSettings PassowrdSettings { get; set; }
        public LockoutSettings LockoutSettings { get; set; }
        public UserSettings UserSettings { get; set; }
    }

    public class UserSettings
    {
        public bool RequireUniqueEmail { get; set; }
    }

    public class PassowrdSettings
    {
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
    }

    public class LockoutSettings
    {
        public bool AllowedForNewUsers { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public int LockoutTimeInMinutes { get; set; }
    }
}
