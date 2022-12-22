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

            try
            {
            var posts = _context.Posts.AsNoTracking().ToList();
            if(posts is null)
            {
                return NotFound("Posts não encontrados!");
            }
            return posts;
                
            }
            catch (Exception)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, 
               "Não conseguimos completar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "Obter produto")]
        
        public ActionResult<Post> Get(int id)
        {
            try
            {
            var posts = _context.Posts.AsNoTracking().FirstOrDefault(post => post.PostId == id);
            if (posts is null)
            {
                return NotFound($"Post com id= {id} não encontrado");
            }
            return posts; 
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Não conseguimos completar sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult Post(Post post)
        {
            try
            {
            if(post is null)
                return BadRequest();

                _context.Posts.Add(post);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto",
                new { id = post.PostId }, post);
                
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
            
        }

        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Post post)
        {
            try
            {
            if(id != post.PostId)
            {
                return BadRequest($"Post com id= {id} não encontrado");
            }

            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(post);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == id);

            if(post is null)
            {
                return NotFound($"Post com id= {id} não encontrado");
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();

            return Ok(post);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }
    
    }
}
