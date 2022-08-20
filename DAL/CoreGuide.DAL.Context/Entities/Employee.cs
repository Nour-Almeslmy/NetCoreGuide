using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Context.Entities
{
    public class Employee : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }
        public double Salary { get; set; }
        [Url]
        public string ProfilePictureURL { get; set; }
        public Guid? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<EmployeeUserRole> UserRoles { get; set; }
        public virtual ICollection<OTP> OTPs { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> UserClaims { get; set; }

    }
}
