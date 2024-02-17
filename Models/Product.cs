using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagyargombfociStore.Models
{
    [Table("Product")]

    public class Product
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }
        [Required]
        public string  Quality { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,0)")]
        public decimal Price { get; set; }

        public string? Image {  get; set; }
        public int CategoryId { get; set; }

        public Categories Categories { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }


        [NotMapped]
        public string CategoryName { get; set; }



    }
}
