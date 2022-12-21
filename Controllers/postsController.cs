using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using System.Linq;

namespace TryitterApi.Controllers
{
    [Route("[controller")]
    [ApiController]

    public class PostsController : ControllerBase
    {
        private readonly MyContext _context;

        public PostsController(MyContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            var posts = _context.Posts.ToList();
            if(posts is null)
            {
                return NotFound("Posts não encontrados!");
            }
            return posts;
        }

        [HttpGet("{id:int}")]
        
        public ActionResult<Post> Get(int id)
        {
            var posts = _context.Posts.FirstOrDefault(post => post.PostId == id);
            if (posts is null)
            {
                return NotFound("Post não encontrado");
            }
            return posts;
        }

    }
}
