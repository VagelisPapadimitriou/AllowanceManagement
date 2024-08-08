using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllowanceManagement.Controllers
{
    [Authorize]
    public class CategoryPercentageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryPercentageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<CategoryPercentage> cpList = _unitOfWork.CategoryPercentage.GetAll().ToList();
            return View(cpList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryPercentage cp)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryPercentage.Add(cp);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Προσθήκη";
                return RedirectToAction("Index", "CategoryPercentage");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CategoryPercentage? CategoryPercentageFromDb = _unitOfWork.CategoryPercentage.Get(cp => cp.CategoryId == id);
            if (CategoryPercentageFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryPercentageFromDb);
        }

        [HttpPost]
        public IActionResult Edit(CategoryPercentage cp)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryPercentage.Update(cp);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Τροποποίηση";
                return RedirectToAction("Index", "CategoryPercentage");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CategoryPercentage? CategoryPercentageFromDb = _unitOfWork.CategoryPercentage.Get(cp => cp.CategoryId == id);
            if (CategoryPercentageFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryPercentageFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            CategoryPercentage? CategoryPercentageFromDb = _unitOfWork.CategoryPercentage.Get(cp => cp.CategoryId == id);
            if (CategoryPercentageFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryPercentage.Remove(CategoryPercentageFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Επιτυχής Διαγραφή";
            return RedirectToAction("Index", "CategoryPercentage");
        }
    }
}
