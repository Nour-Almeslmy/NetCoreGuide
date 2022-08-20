using AutoMapper;
using CoreGuide.BLL.Models.Accounts.ForgetPassword.Input;
using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.MapperService
{
    internal class MapperService : IMapperService
    {
        #region Props
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        public MapperService(
            IMapper mapper,
            IEmployeeRepository employeeRepository
            )
        {
            //var mapperConfig = ConfigMaps();
            //_mapper = mapperConfig.CreateMapper();
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        private MapperConfiguration ConfigMaps()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserInput, Employee>()
                    .ForMember(e => e.PhoneNumber, opts => opts.MapFrom(r => r.Dial));
            }
            );
        }
        public Employee Map(RegisterUserInput input)
        {
            return _mapper.Map<Employee>(input);
        }

        public async Task<OTP> Map(ForgetPasswordInput input)
        {
            var employee = await _employeeRepository.GetByUserNameAsync(input.UserName);
            var rnd = new Random();
            return new OTP()
            {
                Email = employee.Email,
                Dial = employee.PhoneNumber,
                EmployeeId = employee.Id,
                IsUsed = false,
                CreationDate = DateTime.Now,
                Code = rnd.Next(100000, 999999).ToString()
            };
        }
    }
}
