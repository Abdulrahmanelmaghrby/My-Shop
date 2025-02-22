using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myshop.DataAccess.Implementaion;
using Myshop.Entities.Repositories;
using Myshop.Entities.ViewModels;
using System.Security.Claims;

namespace Myshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //inserting the proberty of the the view model to addon it
        public ShoppingCartViewModel shoppingCartViewModel { get; set; }

        public decimal Total { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ;
        }
        public IActionResult Index()
        {
            var calmsIdentety = (ClaimsIdentity)User.Identity;
            var clam = calmsIdentety.FindFirst(ClaimTypes.NameIdentifier);

            //addin the user ID which is signing in to the Chech out  view (TO buy)
            shoppingCartViewModel = new ShoppingCartViewModel()
            {
                  CartsList= _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==clam.Value, Includeword: "Product")  
            };

            foreach (var item in shoppingCartViewModel.CartsList)
            {
                shoppingCartViewModel.TotalCarts += (item.Count * item.Product.Price);
            }
            return View(shoppingCartViewModel);
        }
        public IActionResult Plus( int cartid) 
        {
            var shoppingCart = _unitOfWork.ShoppingCart.GetFristorDefault(x => x.Id == cartid);
            _unitOfWork.ShoppingCart.IncreaseCount(shoppingCart, 1);
            _unitOfWork.Complete();
            

            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartid)
        {
            var shoppingCart = _unitOfWork.ShoppingCart.GetFristorDefault(x => x.Id == cartid);
            if (shoppingCart.Count<1)
            {
                _unitOfWork.ShoppingCart.Remove(shoppingCart);
                _unitOfWork.Complete();
                return RedirectToAction("Index","Home");
               
            }
            else
            {
                _unitOfWork.ShoppingCart.DecreaseCount(shoppingCart, 1);
            }
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
        public IActionResult RemoveItem(int cartid)
        {
            var shoppingCart = _unitOfWork.ShoppingCart.GetFristorDefault(x => x.Id == cartid);
            _unitOfWork.ShoppingCart.Remove(shoppingCart);
            _unitOfWork.Complete();


            return RedirectToAction("Index");
        }

    }
}
