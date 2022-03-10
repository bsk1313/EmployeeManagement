using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string _allowedDomain;
        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this._allowedDomain = allowedDomain; 
        }
        public override bool IsValid(object value)
        {
            string[] emailDomain = value.ToString().Split('@');
            return emailDomain[1].ToString().ToUpper() == _allowedDomain.ToUpper();
        }
    }
}
