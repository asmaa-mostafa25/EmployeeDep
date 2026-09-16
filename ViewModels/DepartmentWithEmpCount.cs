using System.ComponentModel.DataAnnotations;

namespace EmployeeDep.ViewModels
{
    public class DepartmentWithEmpCount
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string ManagerName { get; set; }

        public int EmpCount { get; set; }

    }
}
