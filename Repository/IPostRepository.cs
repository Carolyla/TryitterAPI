using TryitterApi.Models;

namespace TryitterApi.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetByCreated();
    }
}