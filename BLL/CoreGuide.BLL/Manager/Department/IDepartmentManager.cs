using CoreGuide.BLL.Models.Department.Input;
using CoreGuide.Common.Entities.Output;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Manager.Department
{
    public interface IDepartmentManager
    {
        Task<BaseOutput> AddAsync(AddDepratmentInput input, CancellationToken cancellationToken);
    }
}