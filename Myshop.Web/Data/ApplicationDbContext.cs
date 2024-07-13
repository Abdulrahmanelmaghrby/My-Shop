using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Myshop.Web.Data;
using Myshop.Web.Models;

namespace Myshop.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }


    }
    
}
