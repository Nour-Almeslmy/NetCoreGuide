using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators.CustomValidators
{
    internal interface ICustomValidatorsService
    {
        Task<bool> DoesDepartmentExist(int id, CancellationToken cancellationToken);
        Task<bool> DoesManagerExist(Guid id, CancellationToken cancellationToken);
        Task<bool> DoesEmployeeExist(string userName, CancellationToken cancellationToken);
        bool IsDialValid(string dial);
        bool IsEmailValid(string email);
        bool IsValidName(string name);
        bool ValidatePassword(string password);
        Task<bool> IsCurrentPasswordCorrect(string password);
    }
}
