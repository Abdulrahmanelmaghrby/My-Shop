using Myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.Entities.Repositories
{/// <summary>
/// we Created an interface for all models to add its spasific functions which is not in (IGenericRepository)
/// </summary>
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        void Update(Category category);
    }
}
