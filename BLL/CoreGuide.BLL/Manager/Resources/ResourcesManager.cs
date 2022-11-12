using CoreGuide.BLL.Business.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Manager.Resources
{
    internal class ResourcesManager : IResourcesManager
    {
        private readonly IStringLocalizer<ResourcesManager> _stringLocalizer;

        public ResourcesManager(IStringLocalizer<ResourcesManager> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public string SayHello()
        {
            return ErrorMessages.Hello;
        }

        public string SayHelloJSON()
        {
            return _stringLocalizer.GetString("Hello");
        }

    }
}
