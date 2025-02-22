
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public String Name { get; set; }

        public String address { get; set; }
        public String City { get; set; }
    }
}
