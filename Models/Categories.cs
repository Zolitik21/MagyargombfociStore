using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagyargombfociStore.Models
{
    [Table("Categories")]



    public class Categories
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string CategoryName{ get; set; }
        
        public List<Product> Products { get; set; }



    }
}
