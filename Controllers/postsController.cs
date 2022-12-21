using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var posts = _context.Posts.AsNoTracking().ToList();
            if(posts is null)
            {
                return NotFound("Posts não encontrados!");
            }
            return posts;
        }

        [HttpGet("{id:int}", Name = "Obter produto")]
        
        public ActionResult<Post> Get(int id)
        {
            var posts = _context.Posts.AsNoTracking().FirstOrDefault(post => post.PostId == id);
            if (posts is null)
            {
                return NotFound("Post não encontrado");
            }
            return posts;
        }

        [HttpPost]
        public ActionResult Post(Post post)
        {
            if(post is null)
                return BadRequest();

                _context.Posts.Add(post);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto",
                new { id = post.PostId }, post);
            
        }
        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Post post)
        {
            if(id != post.PostId)
            {
                return BadRequest("Produto não encontrado!");
            }

            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(post);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == id);

            if(post is null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();

            return Ok(post);
        }
    
    }
}
