using Myshop.DataAccess.Data;
using Myshop.Entities.Models;
using Myshop.Entities.Repositories;
using Myshop.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.DataAccess.Implementaion
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int IncreaseCount(ShoppingCart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
        public int DecreaseCount(ShoppingCart cart, int count)
        {
            cart.Count -= count;
            return cart.Count;
        }

       
    }
}
