using Microsoft.AspNetCore.Mvc;
using Myshop.Entities.Repositories;
using Myshop.Entities.ViewModels;

namespace Myshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var product =_unitOfWork.Product.GetAll();
            return View(product);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart obj = new ShoppingCart()
            {
                Product= _unitOfWork.Product.GetFristorDefault(x => x.Id == id,Includeword:"Category"),
                Count=1

            };
            return View(obj);
        }
    }
}
