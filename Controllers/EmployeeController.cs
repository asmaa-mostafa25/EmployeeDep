using EmployeeDep.Models;
using EmployeeDep.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDep.Controllers
{

    public class EmployeeController : Controller
    {
        AppDbContext _appDbContext = new AppDbContext();

        public async Task<IActionResult> Index()
        {
            var employees = await _appDbContext.Employees
                .Select(e => new EmployeeWithDepartmentViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    JobTitle = e.JobTitle,
                    DepartmentName = e.Department.Name
                })
                .ToListAsync();

            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            var departments = await _appDbContext.Departments.ToListAsync();

            var vm = new EmployeeViewModel
            {
                Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList()
            };

            return View("AddEmployee", vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _appDbContext.Departments.ToListAsync();

                viewModel.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();

                return View("AddEmployee", viewModel);
            }


            var employeeExists = await _appDbContext.Employees
                .AnyAsync(e =>
                    e.Name.ToLower() == viewModel.Name.ToLower() &&
                    e.DepartmentId == viewModel.DepartmentId);

            if (employeeExists)
            {
                ModelState.AddModelError(
                    "Name",
                    "An employee with the same name already exists in this department.");

                var departments = await _appDbContext.Departments.ToListAsync();

                viewModel.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();

                return View("AddEmployee", viewModel);
            }


            var employee = new Employee
            {
                Name = viewModel.Name,
                Age = viewModel.Age,
                Salary = viewModel.Salary,
                JobTitle = viewModel.JobTitle,
                DepartmentId = viewModel.DepartmentId
            };


            _appDbContext.Employees.Add(employee);

            await _appDbContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            var departments = await _appDbContext.Departments
                .ToListAsync();

            var vm = new EmployeeViewModel
            {
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                JobTitle = employee.JobTitle,
                DepartmentId = employee.DepartmentId,

                Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = d.Id == employee.DepartmentId
                }).ToList()
            };

            return View("EditEmployee", vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(
    int id,
    EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _appDbContext.Departments
                    .ToListAsync();

                viewModel.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = d.Id == viewModel.DepartmentId
                }).ToList();

                return View("EditEmployee", viewModel);
            }

            var employee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            var employeeExists = await _appDbContext.Employees
                .AnyAsync(e =>
                    e.Id != id &&
                    e.Name.ToLower() == viewModel.Name.ToLower() &&
                    e.DepartmentId == viewModel.DepartmentId);

            if (employeeExists)
            {
                ModelState.AddModelError(
                    "Name",
                    "An employee with the same name already exists in this department.");

                var departments = await _appDbContext.Departments
                    .ToListAsync();

                viewModel.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = d.Id == viewModel.DepartmentId
                }).ToList();

                return View("EditEmployee", viewModel);
            }

            employee.Name = viewModel.Name;
            employee.Age = viewModel.Age;
            employee.Salary = viewModel.Salary;
            employee.JobTitle = viewModel.JobTitle;
            employee.DepartmentId = viewModel.DepartmentId;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            _appDbContext.Employees.Remove(employee);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
