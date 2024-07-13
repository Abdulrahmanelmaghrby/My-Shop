using Microsoft.AspNetCore.Mvc;
using Myshop.Web.Data;
using Myshop.Web.Models;

namespace Myshop.Web.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
                _context=context;
        }
        public IActionResult Index()
        {
            var categories=_context.Categories.ToList();
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
                _context.Categories.Add(category);
                _context.SaveChanges();
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
            var category= _context.Categories.Find(id);
            
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
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
            var category = _context.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteModel(int? id)
        {
           var cat= _context.Categories.Find(id);

            if (cat == null)
                NotFound();

            _context.Categories.Remove(cat);
            _context.SaveChanges();
            TempData["Delete"] = "Category is deleted Succsefully";
            return RedirectToAction("Index");
        }
    }
}
