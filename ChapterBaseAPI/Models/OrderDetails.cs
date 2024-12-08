using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChapterBaseAPI.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } 

        public Guid BookId { get; set; }
        public Book Book { get; set; }  
        public int Quantity { get; set; }
        public decimal Price { get; set; }  

        public decimal TotalPrice => Quantity * Price;
    }
}