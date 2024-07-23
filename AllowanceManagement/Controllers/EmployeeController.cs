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
            List<Employee> employeesList = _unitOfWork.Employee.GetAllEmployeesWithRanksAndCategories().ToList();
            return View(employeesList);
        }

        public IActionResult Create()
        {
            PopulateRankWithDuty();
            PopulateCategoryWithPercentage();
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


        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Employee? employeeFromDb = _unitOfWork.Employee.Get(
                emp => emp.AM == id,
                include: query =>query.Include(emp => emp.RankAmount)
                                      .Include(emp => emp.CategoryPercentage)
                );

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
        }

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

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["error"] = "Παρακαλώ επιλέξτε ένα αρχείο.";
                return RedirectToAction("Index");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".txt")
            {
                TempData["error"] = "Παρακαλώ επιλέξτε ένα αρχείο κειμένου (.txt).";
                return RedirectToAction("Index");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                UploadDate = DateTime.Now,
                FilePath = filePath
            };

            _unitOfWork.UploadedFile.Add(uploadedFile);
            _unitOfWork.Save();

            TempData["success"] = "Το αρχείο ανέβηκε επιτυχώς.";
            return RedirectToAction("Index");
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

    }


}
