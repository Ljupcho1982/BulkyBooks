using BulkyBook.Model.ViewModels;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBooksWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {


        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()

            {
                Product = new(),

                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {

                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {

                    Text = i.Name,
                    Value = i.Id.ToString()
                }),


            };

            if (id == null || id == 0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }

            else
            {

                productVM.Product = _unitOfWork.Product.GetFristorDefault(u => u.Id == id);
                return View(productVM);
            }



        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {


            {

            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImageUrl != null)
                    {

                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    obj.Product.ImageUrl = @"\Images\products" + filename + extension;
                }

                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);

                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {

        //        return NotFound();
        //    }

        //    var categoryFromDb = _unitOfWork.CoverType.GetFristorDefault(u => u.Id == id);

        //    if (categoryFromDb == null)
        //    {

        //        return NotFound();
        //    }


        //    return View(categoryFromDb);
        //}

        ////Post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeletePost(int? id)
        //{
        //    var obj = _unitOfWork.CoverType.GetFristorDefault(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }


        //    _unitOfWork.CoverType.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category deleted success";
        //    return RedirectToAction("Index");


        //}

        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = ProductList });


        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFristorDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion




    }

}

