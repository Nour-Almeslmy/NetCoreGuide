using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Context.Entities
{
    public class OTP
    {

        [Key]
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        [StringLength(50)]
        public string Dial { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public bool IsUsed { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
