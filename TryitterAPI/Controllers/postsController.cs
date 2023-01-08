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
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public PostsController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Retorna todos os posts do banco de dados.
        /// </summary>
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
        /// Retorna todos os posts do banco de dados a partir do mais recente para o mais antigo.
        /// </summary>
        [HttpGet("OrdemDeCriação")]
        public ActionResult<IEnumerable<Post>> GetPostCreated()
        {
            return _uof.PostRepository.GetByCreated().ToList();
        }
        /// <summary>
        /// Exibe um post através de seu Id. Digite o número do  Id no parâmetro e clique em executar.
        /// </summary>
        /// <returns>Retorna um objeto com as informações do post de Id conrrespondente</returns>
        [HttpGet("{id:int}")]
        
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
        /// <summary>
        /// Cria um novo post no banco de dados
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /posts
        ///     {
        ///        "title": "um novo título",
        ///        "content": "um novo conteúdo interessante",
        ///        "imageUrl": "represnta.jpg",
        ///        "studentId": 2
        ///     }
        ///
        /// OBS: Não precisa inserir o Id do post, é criado automaticamente,
        /// mas é preciso inserir o Id do estudante corretamente
        /// </remarks>
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
        /// <summary>
        /// Atualiza as informações do post no banco de dados pelo seu Id
        /// </summary>
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
        /// <summary>
        /// Deleta um  post do banco de dados pelo seu Id
        /// </summary>
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
