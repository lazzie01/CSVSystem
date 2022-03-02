using CSVProject.Server.Models;
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
    public class CsvController : ControllerBase
    {
        private readonly ICsvRepository csvRepository;

        public CsvController(ICsvRepository csvRepository)
        {
            this.csvRepository = csvRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Csv>>> GetAllCsvs()
        {
            try
            {
                return Ok(await csvRepository.GetAllCsvs());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Csv>> GetCsv(int id)
        {
            try
            {
                var result = await csvRepository.GetCsv(id);
                if (result == null)
                    return NotFound();
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Csv>> CreateCsv(Csv csv)
        {
            try
            {
                if (csv == null)
                    return BadRequest();

                var csvFound = await csvRepository.GetCsvByFileName(csv.FileName);

                if (csvFound != null)
                {
                    ModelState.AddModelError("FileName", "File Name already exists");
                    return BadRequest(ModelState);
                }

                var createdCsv = await csvRepository.AddCsv(csv);

                return CreatedAtAction(nameof(GetCsv), new { id = createdCsv.Id }, createdCsv);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new csv record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Csv>> UpdateCsv(int id, Csv csv)
        {
            try
            {
                if (id != csv.Id)
                    return BadRequest("Csv Id mismatch");

                var csvToUpdate = await csvRepository.GetCsv(id);

                if (csvToUpdate == null)
                {
                    return NotFound($"Csv with Id = {id} not found");
                }

                return await csvRepository.UpdateCsv(csv);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating csv record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCsv(int id)
        {
            try
            {

                var csvToDelete = await csvRepository.GetCsv(id);

                if (csvToDelete == null)
                {
                    return NotFound($"Csv with Id = {id} not found");
                }

                await csvRepository.DeleteCsv(id);

                return Ok($"Csv with Id = {id} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating csv record");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Csv>>> Search(string fileName, int? id)
        {
            try
            {
                var result = await csvRepository.Search(fileName, id);

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
