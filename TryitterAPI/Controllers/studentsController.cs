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

        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> Get()
        {
            var student =  _uof.StudentRepository.Get().ToList();
            if (student is null)
            {
                return NotFound("Usuários não encontrados!");
            }
            var studentDTO = _mapper.Map<List<StudentDTO>>(student);
            return studentDTO;
        }

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
