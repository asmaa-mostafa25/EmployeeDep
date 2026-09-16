using EmployeeDep.CustomValidationsAttributes;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDep.ViewModels
{
    public class AddDepartmentViewModel
    {
        [Required(ErrorMessage ="The name shouldnt be empty")]
       [StringLength(30, MinimumLength = 3, ErrorMessage = "The name should be between 3 and 30 characters")]
        [UniqueDepartmentName]
        public string Name { get; set; }
        [Required(ErrorMessage ="The Manager Name shouldnt be empty")]
        public string ManagerName { get; set; }
    }
}
