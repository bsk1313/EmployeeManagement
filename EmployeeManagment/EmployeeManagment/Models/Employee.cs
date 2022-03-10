using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage ="Invalid Email format")]
        [Display(Name ="Office Email")]
        [MaxLength(100, ErrorMessage = "Email cannot be exceed 100 characters")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }

        public string PhotoPath { get; set; }
    }
}
