using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TryitterApi.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TryitterApi.Repository;

namespace TryitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public PostsController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> Get()
        {

            try
            {
            var posts = _uof.PostRepository.Get().ToList();
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
        /// <summary>
        /// Exibe um post através de seu Id. digite o número do post e clique em executar.
        /// </summary>
        /// <returns>Retorna um objeto com as informações do post de Id conrrespondente</returns>
        [HttpGet("{id:int}", Name = "Obter produto")]
        
        public ActionResult<PostDTO> Get(int id)
        {
            try
            {
            var posts = _uof.PostRepository.GetById(post => post.PostId == id);
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

                _uof.PostRepository.Add(post);
                _uof.Commit();

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

            _uof.PostRepository.Update(post);
            _uof.Commit();

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
            var post = _uof.PostRepository.GetById(p => p.PostId == id);

            if(post is null)
            {
                return NotFound($"Post com id= {id} não encontrado");
            }
            _uof.PostRepository.Delete(post);
            _uof.Commit();

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
