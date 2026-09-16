using EmployeeDep.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public decimal Salary { get; set; }

    public string JobTitle { get; set; }

    public string? Address { get; set; }

    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }

    public Department Department { get; set; }
}