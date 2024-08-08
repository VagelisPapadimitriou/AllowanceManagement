using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

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
            List<Employee> employeesList = _unitOfWork.Employee.GetAllEmployeesWithRanksAndCategories().ToList();
            return View(employeesList);
        }

        [Authorize]
        public IActionResult SeaDayManagement()
        {
            List<Employee> employeesList = _unitOfWork.Employee.GetAllEmployeesWithRanksAndCategories().ToList();
            PopulateFiles();

            return View(employeesList);
        }

        [Authorize]
        public IActionResult Create()
        {
            PopulateRankWithDuty();
            PopulateCategoryWithPercentage();
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            //custom validation
            //if (emp.FirstName == "test")
            //{
            //    ModelState.AddModelError("FirstName", "Το όνομα δεν μπορεί να είναι test");
            //}

            if (_unitOfWork.Employee.Get(e => e.AM == emp.AM) != null)
            {
                ModelState.AddModelError("AM", "Αυτός ο Αριθμός Μητρώου είναι ήδη καταχωρημένος.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(emp);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Προσθήκη Προσωπικού";
                return RedirectToAction("Index", "Employee");
            }

            PopulateRankWithDuty();
            PopulateCategoryWithPercentage();
            return View(emp);

        }

        [Authorize]
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

            PopulateRankWithDuty();
            PopulateCategoryWithPercentage();
            return View(employeeFromDb);
        }

        [Authorize]
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

            PopulateRankWithDuty();
            PopulateCategoryWithPercentage();
            return View(emp);

        }

        [Authorize]
        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Employee? employeeFromDb = _unitOfWork.Employee.GetEmployeeWithRankAndCategorie(id);

            if (employeeFromDb == null)
            {
                return NotFound();
            }

            return View(employeeFromDb);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public IActionResult AddSeaDay(string id)
        {
            var employee = _unitOfWork.Employee.Get(e => e.AM == id);
            if (employee != null)
            {
                employee.SeaDay += 1;
                _unitOfWork.Employee.Update(employee);
                _unitOfWork.Save();
                TempData["success"] = "Πλεύσιμες ημέρες αυξήθηκαν.";
            }
            return RedirectToAction("Index", "Employee");
            //Json(employee);

        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveSeaDay(string id)
        {
            var employee = _unitOfWork.Employee.Get(e => e.AM == id);
            if (employee != null && employee.SeaDay > 0)
            {
                employee.SeaDay -= 1;
                _unitOfWork.Employee.Update(employee);
                _unitOfWork.Save();
                TempData["success"] = "Πλεύσιμες ημέρες μειώθηκαν.";
            }
            return RedirectToAction("Index", "Employee");
        }

        [Authorize]
        [HttpPost]
        public IActionResult IncreaseSeaDays([FromBody] JsonElement data)
        {
            if (data.ValueKind != JsonValueKind.Object || !data.TryGetProperty("employeeIds", out var employeeIdsElement) || !data.TryGetProperty("days", out var daysElement))
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            var employeeIds = JsonSerializer.Deserialize<List<string>>(employeeIdsElement.GetRawText());
            int days = daysElement.GetInt32();

            if (employeeIds == null || employeeIds.Count == 0 || days < 1)
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            foreach (var id in employeeIds)
            {
                var employee = _unitOfWork.Employee.Get(e => e.AM == id);
                if (employee != null)
                {
                    employee.SeaDay += days;
                    _unitOfWork.Employee.Update(employee);
                }
            }
            _unitOfWork.Save();
            TempData["success"] = "Πλεύσιμες ημέρες αυξήθηκαν για το επιλεγμένο προσωπικό.";
            return Json(new { success = true });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DecreaseSeaDays([FromBody] JsonElement data)
        {
            if (data.ValueKind != JsonValueKind.Object || !data.TryGetProperty("employeeIds", out var employeeIdsElement) || !data.TryGetProperty("days", out var daysElement))
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            var employeeIds = JsonSerializer.Deserialize<List<string>>(employeeIdsElement.GetRawText());
            int days = daysElement.GetInt32();

            if (employeeIds == null || employeeIds.Count == 0 || days < 1)
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            foreach (var id in employeeIds)
            {
                var employee = _unitOfWork.Employee.Get(e => e.AM == id);
                if (employee != null)
                {
                    if (employee.SeaDay >= days)
                    {
                        employee.SeaDay -= days;
                        _unitOfWork.Employee.Update(employee);
                    }
                }
            }
            _unitOfWork.Save();
            TempData["success"] = "Πλεύσιμες ημέρες μειώθηκαν για το επιλεγμένο προσωπικό.";
            return Json(new { success = true });
        }


        private void PopulateRankWithDuty()
        {
            var rankAmounts = _unitOfWork.RankAmount.GetAll().Select(r => new SelectListItem
            {
                Value = r.RankAmountId.ToString(),
                Text = $"{r.Rank} / {r.Duty}"
            }).ToList();
            ViewBag.RankAmounts = rankAmounts;
        }

        private void PopulateCategoryWithPercentage()
        {
            var categories = _unitOfWork.CategoryPercentage.GetAll().Select(cp => new SelectListItem
            {
                Value = cp.CategoryId.ToString(),
                Text = $"{cp.Category} / {cp.Description}"
            }).ToList();
            ViewBag.Categories = categories;
        }

        private void PopulateFiles()
        {
            var filesList = _unitOfWork.UploadedFile.GetFileList().Select(f => new SelectListItem
            {
                Value = f.FileId.ToString(),
                Text = f.FileName
            }).ToList();
            ViewBag.Files = filesList;
        }

    }


}
