using System.ComponentModel.DataAnnotations;

namespace EmployeeDep.CustomValidationsAttributes
{
    public class SalaryAgeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as EmployeeDep.ViewModels.EmployeeViewModel;

            if (employee == null)
                return ValidationResult.Success;

            if (employee.Salary > 20000 && employee.Age < 22)
            {
                return new ValidationResult(
                    "Employee age must be at least 22 when salary is greater than 20000."
                );
            }

            return ValidationResult.Success;
        }
    }
}