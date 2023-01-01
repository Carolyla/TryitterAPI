using TryitterApi.Context;
using TryitterApi.Models;

namespace TryitterApi.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(MyContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Post> GetByCreated()
        {
            return Get().OrderBy(e=> e.CreatedAt).ToList();
        }
    }
}