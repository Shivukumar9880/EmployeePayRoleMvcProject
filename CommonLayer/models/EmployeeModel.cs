using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.models
{
    public class EmployeeModel
    {
       
        [Required(ErrorMessage = "{0} should not be empty")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First name start with Cap and Should have minimum 3 character")]
        [RegularExpression(@"^[A-Z]{1}[a-zA-Z ]{2,}$", ErrorMessage = "First name is not valid")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required]
        public string ImagePath { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Salary is not valid")]
        public decimal Salary { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string Notes { get; set; }
    }
}
