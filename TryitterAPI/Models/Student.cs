using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterApi.Models
{
    public class Student
    {
        public Student()
        {
            Posts = new Collection<Post>();
        }
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Máximo de caracters atingido")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 4)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set;}
    }
    
}