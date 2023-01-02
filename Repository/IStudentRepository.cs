using TryitterApi.Models;

namespace TryitterApi.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> GetStudantsAndPosts();
    }
}