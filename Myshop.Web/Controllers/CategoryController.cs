using Microsoft.AspNetCore.Mvc;
using Myshop.DataAccess.Data;
using Myshop.DataAccess.Implementaion;
using Myshop.Entities.Models;
using Myshop.Entities.Repositories;
//using Myshop.DataAccess.Data;
//using Myshop.Entities.Models;

namespace Myshop.Web.Controllers
{

    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
                _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            //var categories=_context.Categories.ToList();
          var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
        public IActionResult Create() 
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        { 
            if (ModelState.IsValid)
            {
                //_context.Categories.Add(category);
                _unitOfWork.Category.Add(category);
                //_context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Create"] = "Category is created Succsefully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null||id==0)
            {
                return BadRequest();
            }
            //var category= _context.Categories.Find(id);
            var category = _unitOfWork.Category.GetSingleOrdefault(x => x.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Update(category);
                _unitOfWork.Category.Update(category);
                //_context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Update"] = "Category is updated Succsefully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            //var category = _context.Categories.Find(id);
            var category=_unitOfWork.Category.GetSingleOrdefault( x => x.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteModel(int? id)
        {
           //var cat= _context.Categories.Find(id);
           var cat=_unitOfWork.Category.GetSingleOrdefault(y => y.Id == id);

            if (cat == null)
                NotFound();

            //_context.Categories.Remove(cat);
            _unitOfWork.Category.Remove(cat);
            //_context.SaveChanges();
            _unitOfWork.Complete();
            TempData["Delete"] = "Category is deleted Succsefully";
            return RedirectToAction("Index");
        }
    }
}
