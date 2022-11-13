using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Utilities.GuideFillerService;
using CoreGuide.BLL.Business.Utilities.MapperService;
using CoreGuide.BLL.Models.Department.Input;
using CoreGuide.Common.Entities.Output;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Manager.Department
{
    internal class DepartmentManager : IDepartmentManager
    {
        private readonly IGuideFillerService _guideFillerService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IGuideUnitOfWork _guideUnitOfWork;
        private readonly IMapperService _mapperService;

        public DepartmentManager(
            IGuideFillerService guideFillerService,
            IDepartmentRepository departmentRepository,
            IGuideUnitOfWork guideUnitOfWork,
            IMapperService mapperService
            )
        {
            _guideFillerService = guideFillerService;
            _departmentRepository = departmentRepository;
            _guideUnitOfWork = guideUnitOfWork;
            _mapperService = mapperService;
        }

        public async Task<BaseOutput> AddAsync(AddDepratmentInput input, CancellationToken cancellationToken)
        {
            try
            {
                var dbDepartment = _mapperService.Map(input);
                var result = await _departmentRepository.AddAsync(dbDepartment, cancellationToken);
                var commits = await _guideUnitOfWork.SubmitAsync(cancellationToken);
                await Task.Delay(10000,cancellationToken);
                return _guideFillerService.FillSuccessOutput<BaseOutput>(BusinessStrings.Resources.ErrorMessagesKeys.RegistrationSuccess);
            }
            catch (Exception ex)
            {
                return _guideFillerService.FillSystemErrorOutput<BaseOutput>();
            }
        }
    }
}
