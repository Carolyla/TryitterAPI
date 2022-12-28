using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TryitterApi.DTOs;

namespace TryitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class PostsController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public PostsController(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> Get()
        {

            try
            {
            var posts = _context.Posts.ToList();
            if(posts is null)
            {
                return NotFound("Posts não encontrados!");
            }
            var postsDTO = _mapper.Map<List<PostDTO>>(posts);
            return postsDTO;
                
            }
            catch (Exception)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, 
               "Não conseguimos completar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "Obter produto")]
        
        public ActionResult<PostDTO> Get(int id)
        {
            try
            {
            var posts = _context.Posts.FirstOrDefault(post => post.PostId == id);
            if (posts is null)
            {
                return NotFound($"Post com id= {id} não encontrado");
            }
                var postsDTO = _mapper.Map<PostDTO>(posts);
            
                return postsDTO; 
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Não conseguimos completar sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult Post(PostDTO postDto)
        {
            try
            {
            var post = _mapper.Map<Post>(postDto);    
            if(post is null)
                return BadRequest();

                _context.Posts.Add(post);
                _context.SaveChanges();

            var postDTO = _mapper.Map<PostDTO>(post);


                return new CreatedAtRouteResult("ObterProduto",
                new { id = post.PostId }, postDTO);
                
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
