using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterApi.Models
{
    public class Post
    {   
        [Key]
        public int PostId { get; set; }
        [Required]
        
        [MaxLength(80, ErrorMessage = "Número de caracters excedido")]
        public string? Title { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Número de caracters excedido")]
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        
        public int StudentId { get; set; }

        public Student? Studant { get; set; }
        
    }

}