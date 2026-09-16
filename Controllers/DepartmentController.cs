using EmployeeDep.Models;
using EmployeeDep.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDep.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext _appDbContext = new AppDbContext();
        public async Task<IActionResult> Index()
        {
            var vm = await _appDbContext.Departments
                .Select(r => new DepartmentWithEmpCount
                {
                    Id = r.Id,
                    Name = r.Name,
                    ManagerName = r.ManagerName,
                    EmpCount = r.Employees.Count,
                })
                .ToListAsync();

            return View(vm);
        }

        public async Task<IActionResult> GetDepById(int id)
        {
            var dep = await _appDbContext.Departments
                .FirstOrDefaultAsync(x => x.Id == id);

            return View("GetById", dep);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View("AddDepartment");
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(
            AddDepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddDepartment", viewModel);
            }

            var dep = new Department
            {
                Name = viewModel.Name,
                ManagerName = viewModel.ManagerName
            };

            _appDbContext.Departments.Add(dep);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            var dep = await _appDbContext.Departments
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dep == null)
            {
                return NotFound();
            }
            var viewModel = new AddDepartmentViewModel
            {
                
                Name = dep.Name,
                ManagerName = dep.ManagerName
            };
            return View("EditDepartment", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditDepartment(int id, AddDepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("EditDepartment", viewModel);
            }

            var dep = await _appDbContext.Departments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dep == null)
                return NotFound();

            dep.Name = viewModel.Name;
            dep.ManagerName = viewModel.ManagerName;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dep = await _appDbContext.Departments
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dep == null)
                return NotFound();
            _appDbContext.Departments.Remove(dep);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ShowEmployees(int id)
        {
            var employees = await _appDbContext.Employees
                .Where(e => e.DepartmentId == id)
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

            return View("ShowEmployees", employees);
        }
    }
}
