using CoreGuide.BLL.Business.Manager.Department;
using CoreGuide.BLL.Models.Department.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentManager _departmentManager;

        public DepartmentsController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] AddDepratmentInput input, CancellationToken cancellationToken)
        {
            var output = await _departmentManager.AddAsync(input, cancellationToken);
            return Ok(output);
        }
    }
}
