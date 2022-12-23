using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TryitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class StudentsController : ControllerBase
    {
        private readonly MyContext _context;

        public StudentsController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var student = await _context.Students.ToListAsync();
            if (student is null)
            {
                return NotFound("Usuários não encontrados!");
            }
            return student;
        }

        [HttpGet("posts")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudantsAndPosts()
        {
            try
            {
            var student = await _context.Students.Include(p => p.Posts).ToListAsync();
            if (student is null)
            {
                return NotFound("Usuários não encontrados!");
            }
            return student;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "Obter Usuario")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            try
            {
            var student = await _context.Students.FirstOrDefaultAsync(post => post.StudentId == id);
            if (student is null)
            {
                return NotFound($"Usuario com id= {id} não encontrado");
            }
            return student;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Não conseguimos completar sua solicitação");
            }
        }

        [HttpPost]
        public  async Task<ActionResult> Post(Student student)
        {
            try
            {
            if (student is null)
                return BadRequest("Não foi possível criar um novo cadastro");

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

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

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

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
            var student = _context.Students.FirstOrDefault(p => p.StudentId == id);

            if (student is null)
            {
                return NotFound($"Usuario com id= {id} não encontrado");
            }
            _context.Students.Remove(student);
            _context.SaveChanges();

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
