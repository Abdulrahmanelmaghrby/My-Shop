﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Myshop.Entities.Models;


namespace Myshop.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


    }
    
}
