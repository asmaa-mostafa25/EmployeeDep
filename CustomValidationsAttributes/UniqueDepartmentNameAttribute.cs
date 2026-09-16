using EmployeeDep.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDep.CustomValidationsAttributes
{
    public class UniqueDepartmentNameAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = new AppDbContext();
            string? name = value.ToString();
            bool exists = context.Departments.Any(x => x.Name == name);
            if (exists)
            {
                return new ValidationResult("Department name already exists");
            }
            return ValidationResult.Success;
        }
    }
}
