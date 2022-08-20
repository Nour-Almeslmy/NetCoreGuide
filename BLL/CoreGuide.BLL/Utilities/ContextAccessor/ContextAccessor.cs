using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.ContextAccessor
{
    internal class ContextAccessor : IContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string Host
        {
            get
            {
                return _httpContextAccessor.HttpContext.Request.Host.ToString();
            }
        }

        public string Scheme
        {
            get
            {
                return _httpContextAccessor.HttpContext.Request.Scheme.ToString();
            }
        }

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return default;

                var userId = user.FindFirstValue(Common.Utilities.Strings.JWTClaimNames.Id);
                if (userId != null)
                    return Guid.Parse(userId);
                return default;
            }
        }
    }
}
