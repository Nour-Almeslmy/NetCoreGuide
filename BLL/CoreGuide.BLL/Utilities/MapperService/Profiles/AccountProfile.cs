using AutoMapper;
using CoreGuide.BLL.Models.Accounts.ForgetPassword.Input;
using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.DAL.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.MapperService.Profiles
{
    internal class AccountProfile : Profile
    {
        public AccountProfile()
        {

            CreateMap<RegisterUserInput, Employee>()
                .ForMember(e => e.PhoneNumber, opts => opts.MapFrom(r => r.Dial))
                .ForMember(e => e.UserName, opts => opts.MapFrom(r => r.Email));
        }
    }
}
