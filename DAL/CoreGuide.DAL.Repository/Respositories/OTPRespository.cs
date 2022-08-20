using CoreGuide.Common.GenericRepository.Respository;
using CoreGuide.DAL.Context;
using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository.Respositories
{
    internal class OTPRespository : Repository<OTP>, IOTPRespository
    {
        public OTPRespository(GuideContext context) : base(context)
        {

        }
    }
}
