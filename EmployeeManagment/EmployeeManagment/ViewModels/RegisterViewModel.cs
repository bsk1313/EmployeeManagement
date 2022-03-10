using EmployeeManagment.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomainAttribute(allowedDomain : "kochharsolutions.net", ErrorMessage = "Email domain must be kochharsolutions.net")]
        public string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        public string City{ get; set; }
    }
}
