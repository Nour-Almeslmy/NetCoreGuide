using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Swagger
    {
        #region Add comments
        /// 1) Add in csproj "<GenerateDocumentationFile>true</GenerateDocumentationFile>" under "PropertyGroup"
        /// 2) In services.AddSwaggerGen add the following
        const string xmlPath = 
            @"var xmlFile = $""{Assembly.GetExecutingAssembly().GetName().Name}.xml"";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);";
        #endregion
    }
}
