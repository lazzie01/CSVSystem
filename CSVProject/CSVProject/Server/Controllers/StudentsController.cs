using CSVProject.DataAccessLayer.Models;
using CSVProject.Server.ViewModels;
using CSVProject.Shared;
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
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult<Student>> GetStudent(string id, string number)
        {
            try
            {
                if (int.TryParse(id, out int csvId) && int.TryParse(number, out int studentNumber))
                {
                    return Ok(await studentRepository.GetStudent(csvId, studentNumber));
                }
                return null;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] StudentViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                    return BadRequest();

                var studentFound = await studentRepository.GetStudent(viewModel.CsvId, viewModel.StudentNumber);

                if (studentFound != null)
                {
                    ModelState.AddModelError("Student", "Student already exists");
                    return BadRequest(ModelState);
                }

                var createdStudent = await studentRepository.AddStudent(viewModel.CsvId, viewModel.ToModel());

                return CreatedAtAction(nameof(GetStudent), new { id = viewModel.CsvId, number = createdStudent.StudentNumber }, createdStudent);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new csv record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, StudentViewModel viewModel)
        {
            try
            {
                if (id != viewModel.CsvId)
                    return BadRequest("Csv Id mismatch");

                var studentToUpdate = await studentRepository.GetStudent(id, viewModel.StudentNumber);

                if (studentToUpdate == null)
                {
                    return NotFound($"Student with Student Number = {viewModel.StudentNumber} not found");
                }

                return await studentRepository.UpdateStudent(id, viewModel.ToModel());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating csv record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id, string number)
        {
            try
            {
                if (int.TryParse(number, out int studentNumber))
                {
                    var studentToDelete = await studentRepository.GetStudent(id, studentNumber);

                    if (studentToDelete == null)
                    {
                        return NotFound($"Student with Student Number = {studentNumber} in Csv with Id = {id} not found");
                    }

                    await studentRepository.DeleteStudent(id, studentNumber);

                    return Ok($"Student with Student Number = {studentNumber} in Csv with Id = {id} deleted");
                }
                return BadRequest();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating csv record");
            }
        }

        [HttpGet("{search}/{id:int}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(int id, string firstname, string surname, string courseCode, string courseDescription, string grade)
        {
            try
            {
                var result = await studentRepository.Search(id, firstname, surname, courseCode, courseDescription, grade);

                if (result.Any())
                    return Ok(result);
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
