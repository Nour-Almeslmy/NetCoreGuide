using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class FilesUpload
    {
        #region Steps
        /// 1) add middleware "app.UseStaticFiles();"
        /// 2) Save in the directory "wwwroot" through property  "WebRootPath" in "IWebHostEnvironment" service
        /// 3) You should initialize "wwwroot" or  property  "WebRootPath" in "IWebHostEnvironment" service will be null,
        ///  You can do it through creating the folder manually, or adding ".UseWebRoot("wwwroot");" in the "ConfigureWebHostDefaults" configurations in the Program file
        #endregion
        #region References
        //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0
        #endregion
    }
}
