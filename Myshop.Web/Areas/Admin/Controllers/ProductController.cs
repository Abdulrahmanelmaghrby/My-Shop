using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Myshop.DataAccess.Data;
using Myshop.DataAccess.Implementaion;
using Myshop.Entities.Models;
using Myshop.Entities.Repositories;
using Myshop.Entities.ViewModels;
//using Myshop.DataAccess.Data;
//using Myshop.Entities.Models;

namespace Myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller 
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {/*
            var categories=_context.Categories.ToList();
            var products = _unitOfWork.Product.GetAll();
            return View(products);
            we will get the data as json in the data table 
            */
           return View();
           
        }
        public IActionResult GetData()
        {
            var products = _unitOfWork.Product.GetAll(Includeword:"Category");
            return Json(new { data = products });
        }
        public IActionResult Create()
        {
            ProductFormViewModel productFormViewModel = new ProductFormViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(productFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductFormViewModel productViewModel,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string Rootpath=_webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var filename = Guid.NewGuid().ToString();
                    var upload =Path.Combine(Rootpath, @"Images\Products");
                    var ext=Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productViewModel.Product.Img= @"Images\Products\"+filename+ext;
                }
                //_context.Categories.Add(product);
                _unitOfWork.Product.Add(productViewModel.Product);
                //_context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Create"] = "Product is created Succsefully";
                return RedirectToAction("Index");
            }
            return View(productViewModel.Product);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            ////var product= _context.Categories.Find(id); 1st
            //var product = _unitOfWork.Product.GetFristorDefault(x => x.Id == id); 
            //return View(product);
            ProductFormViewModel productFormViewModel = new ProductFormViewModel()
            {
                Product = _unitOfWork.Product.GetFristorDefault(x => x.Id == id),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(productFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductFormViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string Rootpath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(Rootpath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    if (productViewModel.Product.Img != null)
                    {
                        var oldImag = Path.Combine(Rootpath, productViewModel.Product.Img.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImag))
                        {
                            System.IO.File.Delete(oldImag);
                        }
                    }
                    
                    using (var fileStream = new FileStream(Path.Combine(upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productViewModel.Product.Img = @"Images\Products\" + filename + ext;
                }
                //_context.Categories.Update(product);
                _unitOfWork.Product.Update(productViewModel.Product);
                //_context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Update"] = "Product is updated Succsefully";
                return RedirectToAction("Index");
            }
            return View(productViewModel.Product);
        }
  /*      will no need it because we use a toastr
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            //var product = _context.Categories.Find(id);
            var product = _unitOfWork.Product.GetFristorDefault(x => x.Id == id);
            return View(product);
        }
  */
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            //var cat= _context.Categories.Find(id);
            var product = _unitOfWork.Product.GetFristorDefault(y => y.Id == id);

            if (product == null)
                return Json(new { success = false, message = "error while Deleting" });

            //_context.Categories.Remove(cat);
            _unitOfWork.Product.Remove(product);

                var oldImag = Path.Combine(_webHostEnvironment.WebRootPath, product.Img.TrimStart('\\'));
                if (System.IO.File.Exists(oldImag))
                {
                    System.IO.File.Delete(oldImag);
                }
           
            //_context.SaveChanges();
            _unitOfWork.Complete();
           // TempData["Delete"] = "Product is deleted Succsefully";
            return Json(new { success = true, message = "the product deleted" });
        }
    }
}
