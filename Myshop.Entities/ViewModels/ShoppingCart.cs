using Myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.Entities.ViewModels
{
     public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1,100,ErrorMessage ="Wrong count ")]
        public int Count { get; set; }
    }
}
