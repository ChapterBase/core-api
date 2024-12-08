using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChapterBaseAPI.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }  
        public Guid BookId { get; set; }
        public Book Book { get; set; }  
        public Guid CustomerId { get; set; }
        public Users Customer { get; set; }
        public int Rating { get; set; }  
        public string Comment { get; set; }  
        public DateTime CreatedAt { get; set; } 
    }
}