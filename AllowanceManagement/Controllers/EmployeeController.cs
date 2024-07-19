using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AllowanceManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Employee> employeesList = _unitOfWork.Employee.GetAll().ToList();
            return View(employeesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            //custom validation
            //if (emp.FirstName == "test")
            //{
            //    ModelState.AddModelError("FirstName", "Το όνομα δεν μπορεί να είναι test");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(emp);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Προσθήκη Προσωπικού";
                return RedirectToAction("Index", "Employee");
            }

            return View();

        }

        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            Employee? employeeFromDb = _unitOfWork.Employee.Get(emp => emp.AM == id);
            if (employeeFromDb == null)
            {
                return NotFound();
            }
            return View(employeeFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Update(emp);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Τροποποίηση Στοιχείων";
                return RedirectToAction("Index", "Employee");
            }

            return View();

        }


        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            Employee? employeeFromDb = _unitOfWork.Employee.Get(emp => emp.AM == id);
            if (employeeFromDb == null)
            {
                return NotFound();
            }
            return View(employeeFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            Employee? employeeFromDb = _unitOfWork.Employee.Get(emp => emp.AM == id);
            if (employeeFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Employee.Remove(employeeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Επιτυχής Διαγραφή Προσωπικού";
            return RedirectToAction("Index", "Employee");
        }

    }
}
