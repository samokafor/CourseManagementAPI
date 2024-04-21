using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories;
using CourseManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        //[Authorize(Roles = "Administrator, Regular")]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Instructor>> GetSingleInstructor(int Id)
        {
            try
            {
                var instructor = await _instructorRepository.GetSingleInstructorAsync(Id);
                if (instructor == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Instructor with ID - {Id} does not exist");
                }
                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching data from database. {ex.Message}");

            }
        }

        //[Authorize(Roles = "Administrator, Regular")]
        [HttpGet("Instructors")]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetAllinstructorsAsync()
        {
            try
            {
                var instructor = await _instructorRepository.GetAllInstructorsAsync();
                if (instructor == null)
                {
                    return StatusCode(StatusCodes.Status302Found, "No instructors have been created yet");
                }
                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching data from database. {ex.Message}");
            }
        }

        //[Authorize(Roles = "Administrator, Regular")]
        [HttpGet("Search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Instructor>>> SearchInstructorsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return BadRequest();
                }

                var result = await _instructorRepository.SearchInstructorsAsync(searchTerm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Oops! {ex.Message}");

            }
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public async Task<ActionResult<Instructor>> CreateInstructorAsync(InstructorDto instructorDto)
        {
            try
            {
                if (instructorDto == null) return BadRequest();
                var existingInstructor = await _instructorRepository.SearchInstructorsAsync(instructorDto.Name);
                if (existingInstructor.Count() != 0)
                {
                    ModelState.AddModelError("Name", $"Instructor with name - {instructorDto.Name} already exists.");
                    return BadRequest(ModelState);
                }

                var newInstructor = await _instructorRepository.AddInstructorAsync(instructorDto);
                return CreatedAtAction(nameof(GetSingleInstructor), new { id = newInstructor.Id }, newInstructor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating instructor. {ex.Message}");

            }
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("Edit/{Id:int}")]
        public async Task<ActionResult<Instructor>> EditInstructorAsync(int Id, InstructorDto instructorDto)
        {
            try
            {
                if (instructorDto == null) return BadRequest();
                var instructorToUpdate = await _instructorRepository.GetSingleInstructorAsync(Id);
                if (instructorToUpdate == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Instructor with ID - {Id} does not exist");
                }
                await _instructorRepository.UpdateInstructorAsync(Id, instructorDto);
                var updatedinstructor = await _instructorRepository.GetSingleInstructorAsync(Id);
                return Ok(updatedinstructor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating Instructor. {ex.Message}");

            }
        }

        //[Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{Id:int}")]
        public async Task<ActionResult> DeleteInstructorAsync(int Id)
        {
            try
            {
                var instructorToDelete = await _instructorRepository.GetSingleInstructorAsync(Id);
                if (instructorToDelete == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Instructor with ID - {Id} does not exist");

                }
                await _instructorRepository.DeleteInstructorAsync(Id);
                return Ok($"{instructorToDelete.Name} has been deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting Instructor from database. {ex.Message}");

            }
        }
    }
}
