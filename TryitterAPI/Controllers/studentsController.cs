using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TryitterApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TryitterApi.Repository;

namespace TryitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Retorna todos os estudantes
        /// </summary>
        [HttpGet]
    
        public ActionResult<IEnumerable<StudentDTO>> Get()
        {
            try
            {
                var student =  _uof.StudentRepository.Get().ToList();
            if (student is null)
            {
                return NotFound("Usuários não encontrados!");
            }
            var studentDTO = _mapper.Map<List<StudentDTO>>(student);
            
            return studentDTO;

            }
            catch (Exception)
            {
                
                return BadRequest();
            }
            
        }
        /// <summary>
        /// Retorna todos os estudantes e suas postagens no nosso blog.
        /// </summary>
        [HttpGet("posts")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudantsAndPosts()
        {
            try
            {
            var student = _uof.StudentRepository.GetStudantsAndPosts().ToList();
            if (student is null)
            {
                return NotFound("Usuários não encontrados!");
            }
            var studentDTO = _mapper.Map<List<StudentDTO>>(student);
            return studentDTO;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="StudantId">Número de identificação o estudante no banco de dados</param>
        [HttpGet("{id:int}", Name = "Obter Usuario")]
        public ActionResult<StudentDTO> Get(int id)
        {
            try
            {
            var student =  _uof.StudentRepository.GetById(post => post.StudentId == id);
            if (student is null)
            {
                return NotFound($"Usuario com id= {id} não encontrado");
            }
                var studentDTO = _mapper.Map<StudentDTO>(student);
                
                return studentDTO;
        

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }

        /// <summary>
        /// Cria um novo estudante
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Studants
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
        public ActionResult Post(Student student)
        {
            try
            {
            if (student is null)
                return BadRequest("Não foi possível criar um novo cadastro");

             _uof.StudentRepository.Add(student);
             _uof.Commit();

            return new CreatedAtRouteResult("ObterUsuario",
            new { id = student.StudentId }, student);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }

        }

        /// <summary>
        /// Atualiza as informações de um estudante no banco de dados
        /// </summary>
        /// <param name="studant"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Student student)
        {
            try
            {
            if (id != student.StudentId)
            {
                return BadRequest($"Usuario com id= {id} não encontrado");
            }

                _uof.StudentRepository.Update(student);
                _uof.Commit();
            return Ok(student);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }
        /// <summary>
        /// Deleta um Student pelo seu Id
        /// </summary>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
            var student = _uof.StudentRepository.GetById(p => p.StudentId == id);

            if (student is null)
            {
                return NotFound($"Usuario com id= {id} não encontrado");
            }
                _uof.StudentRepository.Delete(student);
                _uof.Commit();

            return Ok(student);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }

        
        
    }
}
