using TryitterApi.Models;

namespace TryitterApi.Repository
{
    interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> GetStudantsAndPosts();
    }
}