using CoreGuide.BLL.Business.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Manager.Resources
{
    internal class ResourcesManager : IResourcesManager
    {
        public string SayHello()
        {
            return ErrorMessages.Hello;
        }
    }
}
