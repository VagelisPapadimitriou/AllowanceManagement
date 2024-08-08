using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllowanceManagement.Controllers
{
    [Authorize]
    public class RankAmountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RankAmountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<RankAmount> rankAmountsList = _unitOfWork.RankAmount.GetAll().ToList();
            return View(rankAmountsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RankAmount rankAmount)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RankAmount.Add(rankAmount);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Προσθήκη Βαθμού/Ποσού";
                return RedirectToAction("Index", "RankAmount");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0)
            {
                return NotFound();
            }
            RankAmount? rankAmountFromDb = _unitOfWork.RankAmount.Get(r => r.RankAmountId == id);
            if (rankAmountFromDb == null)
            {
                return NotFound();
            }
            return View(rankAmountFromDb);
        }

        [HttpPost]
        public IActionResult Edit(RankAmount rankAmount)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RankAmount.Update(rankAmount);
                _unitOfWork.Save();
                TempData["success"] = "Επιτυχής Τροποποίηση Βαθμού/Ποσού";
                return RedirectToAction("Index", "RankAmount");
            }

            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            RankAmount? rankAmountFromDb = _unitOfWork.RankAmount.Get(r => r.RankAmountId == id);
            if (rankAmountFromDb == null)
            {
                return NotFound();
            }
            return View(rankAmountFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            RankAmount? rankAmountFromDb = _unitOfWork.RankAmount.Get(r => r.RankAmountId == id);
            if (rankAmountFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.RankAmount.Remove(rankAmountFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Επιτυχής Διαγραφή Βαθμού/Ποσού";
            return RedirectToAction("Index", "RankAmount");
        }

    }
}
