using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.ConfigurationSettings
{
    public class AllowedFileSettings
    {
        public string[] AllowedImageExtensions { get; set; }
        public int MaximumImageSize { get; set; }
    }
}
