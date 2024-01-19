using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.models
{
    public class LoginModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
