using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Context.Entities
{
    public class EmployeeUserRole : IdentityUserRole<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual IdentityRole<Guid> Role { get; set; }
    }
}
