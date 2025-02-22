using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myshop.Entities.Repositories;
using Myshop.Entities.ViewModels;
using System.Security.Claims;

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
        [Authorize]
        public IActionResult Details(int ProductId)
        {

            ShoppingCart obj = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = _unitOfWork.Product.GetFristorDefault(x => x.Id == ProductId, Includeword: "Category"),
                Count = 1

            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var calmsIdentety = (ClaimsIdentity)User.Identity;
            var clam = calmsIdentety.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId= clam.Value;
            //get the existing cart from data base  and add on it 
            ShoppingCart cartobj=_unitOfWork.ShoppingCart.GetFristorDefault(
                u=>u.ApplicationUserId==clam.Value && u.ProductId==shoppingCart.ProductId);

            if (cartobj == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.ShoppingCart.IncreaseCount(cartobj, shoppingCart.Count);
            }

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}
