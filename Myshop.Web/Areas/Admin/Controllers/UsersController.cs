using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myshop.Utilities;
using System.Security.Claims;


namespace Myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =MyIdentityRoles.AdminRole)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var calmsIdentety=(ClaimsIdentity)User.Identity;
            var clam = calmsIdentety.FindFirst(ClaimTypes.NameIdentifier);
            string userID = clam.Value;

            return View(_context.ApplicationUsers.Where(x=>x.Id != userID).ToList());
        }

        public IActionResult LockUnlock(String id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x=>x.Id==id);

            if (user==null)
                return NotFound();

            if (user.LockoutEnd==null || user.LockoutEnd<DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddDays(1);
            }
            else
            {
                user.LockoutEnd= DateTime.Now;
            }

            _context.SaveChanges();
            return RedirectToAction("Index","Users",new {area="Admin"});

        }
    }
}
