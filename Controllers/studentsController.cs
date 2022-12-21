using TryitterApi.Context;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TryitterApi.Controllers
{
    [Route("[controller")]
    [ApiController]

    public class StudentsController : ControllerBase
    {
        private readonly MyContext _context;

        public StudentsController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            var student = _context.Students.ToList();
            if (student is null)
            {
                return NotFound("Usuário não encontrados!");
            }
            return student;
        }

        [HttpGet("posts")]
        public ActionResult<IEnumerable<Student>> GetStudantsAndPosts()
        {
            var student = _context.Students.Include(p => p.Posts).ToList();
            if (student is null)
            {
                return NotFound("Usuário não encontrados!");
            }
            return student;
        }

        [HttpGet("{id:int}", Name = "Obter Usuario")]

        public ActionResult<Student> Get(int id)
        {
            var student = _context.Students.FirstOrDefault(post => post.StudentId == id);
            if (student is null)
            {
                return NotFound("Usuario não encontrado");
            }
            return student;
        }

        [HttpPost]
        public ActionResult Post(Student student)
        {
            if (student is null)
                return BadRequest();

            _context.Students.Add(student);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterUsuario",
            new { id = student.StudentId }, student);

        }
        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest("Usuario não encontrado!");
            }

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(student);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(p => p.StudentId == id);

            if (student is null)
            {
                return NotFound("Usuario não encontrado");
            }
            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok(student);
        }

    }
}
