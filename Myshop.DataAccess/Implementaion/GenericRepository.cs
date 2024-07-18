using Microsoft.EntityFrameworkCore;
using Myshop.DataAccess.Data;
using Myshop.Entities.Models;
using Myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.DataAccess.Implementaion
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
           _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate, string? Includeword)
        {
            IQueryable<T> quary = _dbSet;
            if (predicate != null)
            {
                quary= quary.Where(predicate);
                
            }
            if (Includeword != null)
            {
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary = quary.Include(Includeword);
                }
            }
            return quary.ToList();
        }

        public T GetSingleOrdefault(System.Linq.Expressions.Expression<Func<T, bool>>? predicate, string? Includeword)
        {
            IQueryable<T> quary = _dbSet;
            if (predicate != null)
            {
                quary = quary.Where(predicate);

            }
            if (Includeword != null)
            {//_context.Prouducts.Incluade("Category,logos,users")
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary = quary.Include(item);
                }
            }
            return quary.SingleOrDefault();
        }

        public void Remove(T entity)
        {
           _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
