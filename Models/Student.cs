using System.Collections.ObjectModel;

namespace TryitterApi.Models
{
    public class Student
    {
        public Student()
        {
            Posts = new Collection<Post>();
        }
        public int StudentId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public ICollection<Post>? Posts { get; set;}
    }
    
}