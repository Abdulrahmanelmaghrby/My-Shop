using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myshop.Web.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descreprtion { get; set; }

        public DateTime CreatedTime  {get; set; } = DateTime.Now;


    }
}
