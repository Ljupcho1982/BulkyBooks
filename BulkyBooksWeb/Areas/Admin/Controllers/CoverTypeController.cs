using BulkyBook.Model;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBooksWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {


        private IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();

            return View(objCoverTypeList);
        }

        public IActionResult Create()
        {


            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {


            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Catgory created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }

            var categoryFromDb = _unitOfWork.CoverType.GetFristorDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {

                return NotFound();
            }


            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {


            {

            }
            if (ModelState.IsValid)
            {

                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category edited success";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }

            var categoryFromDb = _unitOfWork.CoverType.GetFristorDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {

                return NotFound();
            }


            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFristorDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }


            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted success";
            return RedirectToAction("Index");


        }

    }
}
