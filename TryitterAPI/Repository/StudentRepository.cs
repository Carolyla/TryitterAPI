using Microsoft.EntityFrameworkCore;
using TryitterApi.Context;
using TryitterApi.Models;

namespace TryitterApi.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(MyContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Student> GetStudantsAndPosts()
        {
            return Get().Include( e=> e.Posts);
        }
    }
}
