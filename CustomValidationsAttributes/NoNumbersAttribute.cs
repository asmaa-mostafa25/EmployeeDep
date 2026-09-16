using System.ComponentModel.DataAnnotations;

namespace EmployeeDep.CustomValidationsAttributes
{
    public class NoNumbersAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string name = value.ToString();

            foreach (char c in name)
            {
                if (char.IsDigit(c))
                {
                    return new ValidationResult(
                        "Name cannot contain numbers."
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}