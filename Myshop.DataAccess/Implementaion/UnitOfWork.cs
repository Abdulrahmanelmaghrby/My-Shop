using Myshop.DataAccess.Data;
using Myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.DataAccess.Implementaion
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        // public CategoryRepository Category { get; private set; }
        // معملتهاش كده عشان(ICategoryRepository) بتحتاج يتعملها implement 
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category=new CategoryRepository(context);

        }

     

        public int Complete()
        {
           return _context.SaveChanges();

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
