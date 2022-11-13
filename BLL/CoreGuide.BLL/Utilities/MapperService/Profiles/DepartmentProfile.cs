using AutoMapper;
using CoreGuide.BLL.Models.Department.Input;
using CoreGuide.DAL.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.MapperService.Profiles
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {

            CreateMap<AddDepratmentInput, Department>()
                .ForMember(d => d.Name, opts => opts.MapFrom(i => i.Name));
        }
    }
}
