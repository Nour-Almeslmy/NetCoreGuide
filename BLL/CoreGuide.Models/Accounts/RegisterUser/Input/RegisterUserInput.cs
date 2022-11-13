using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Accounts.RegisterUser.Input
{
    public record RegisterUserInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Dial { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
        public Guid? ManagerId { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public bool IsAdmin { get; set; }
    }
}
