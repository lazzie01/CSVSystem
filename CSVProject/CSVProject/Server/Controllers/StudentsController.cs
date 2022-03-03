using CSVProject.Server.Models;
using CSVProject.Server.Models.Mapping;
using CSVProject.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents(int id)
        {
            try
            {
                return Ok(await studentRepository.GetAllStudents(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
