using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
