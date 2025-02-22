using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.Entities.Repositories
{//repsitory for all functions needed
    public interface IGenericRepository<T> where T : class
    {
        //_context.Categories.ToList();
        //_context.Categories.Include().ToList(); OR
        //_context.Categories.where(x=>x.id).ToList(); if we used this we need to make func<>

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string? Includeword = null);

        T GetFristorDefault(Expression<Func<T, bool>>? predicate=null, string? Includeword = null);
      //  _context.Categories.Add(category);
        void Add(T entity);
       // _context.Categories.Remove(category);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
