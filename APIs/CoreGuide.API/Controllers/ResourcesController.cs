using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreGuide.BLL.Business.Manager.Resources;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesManager _resourcesManager;

        public ResourcesController(IResourcesManager  resourcesManager)
        {
            _resourcesManager = resourcesManager;
        }

        [HttpGet]
        public IActionResult SayHello()
        {
            return Ok(_resourcesManager.SayHello());
        }
    }
}
