using ASPCoreWebAPICRUD.ModelsModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly MyDBContext context;

        public StudentApiController(MyDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data=await context.Students.ToListAsync();
            return Ok(data);
            //return the data not view
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);
            //kya vo id stduent record mai ha
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> updateStudent(int id,Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await context.Students.FindAsync(id);
            if (std== null)
            {
                return NotFound();
            }
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
