using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Swagger
    {
        /// Swagger (OpenAPI) is a language-agnostic specification for describing REST APIs. 
        /// It allows both computers and humans to understand the capabilities of a REST API without direct access to the source code. Its main goals are to:
        /// 1) Minimize the amount of work needed to connect decoupled services.
        /// 2) Reduce the amount of time needed to accurately document a service.
        ///
        #region Add comments
        /// 1) Add in csproj "<GenerateDocumentationFile>true</GenerateDocumentationFile>" under "PropertyGroup"
        /// 2) In services.AddSwaggerGen add the following
        const string xmlPath = 
            @"var xmlFile = $""{Assembly.GetExecutingAssembly().GetName().Name}.xml"";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);";
        #endregion


        #region References
        /// https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0
        /// https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
        /// https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio#xml-comments
        /// https://github.com/OAI/OpenAPI-Specification
        /// https://dev.to/raviranjanpandey/configure-authorization-in-openapi-swagger-ui-for-jwt-in-asp-net-core-web-api-1p1k
        /// https://dev.to/eduardstefanescu/aspnet-core-swagger-documentation-with-bearer-authentication-40l6
        /// https://mattfrear.com/tag/swagger/
        /// https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1425
        #endregion
    }
}
