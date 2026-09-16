using EmployeeDep.CustomValidationsAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDep.ViewModels
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "The name shouldn't be empty")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "The name should be between 3 and 50 characters")]
        [NoNumbers]
        public string Name { get; set; }

        [Range(20, 60,
            ErrorMessage = "The age should be between 20 and 60")]
        [SalaryAgeValidation]
        public int Age { get; set; }

        [Range(5000, 50000,
            ErrorMessage = "The salary should be between 5000 and 50000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "The job title is required")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        public List<SelectListItem>? Departments { get; set; }
    }
}