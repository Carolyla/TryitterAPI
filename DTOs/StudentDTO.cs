
using AutoMapper;

namespace TryitterApi.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<PostDTO>? Posts { get; set; }
    }

}
        