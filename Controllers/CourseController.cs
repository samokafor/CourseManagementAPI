using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;   
        }
        [Authorize(Roles = "Administrator, Regular")]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Course>> GetSingleCourse(int Id)
        {
            try
            {
                var course = await _courseRepository.GetSingleCourseAsync(Id);
                if (course == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Course with ID - {Id} does not exist");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching data from database. {ex.Message}");

            }
        }
        [Authorize(Roles = "Administrator, Regular")]
        [HttpGet("CourseList")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCoursesAsync()
        {
            try
            {
                var course = await _courseRepository.GetAllCoursesAsync();
                if (course == null)
                {
                    return StatusCode(StatusCodes.Status302Found, "No courses have been created yet");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching data from database. {ex.Message}");
            }
        }

        [Authorize(Roles = "Administrator, Regular")]
        [HttpGet("Search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Course>>> SearchCoursesAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return BadRequest();
                }

                var result = await _courseRepository.SearchCourses(searchTerm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Oops! {ex.Message}");

            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public async Task<ActionResult<Course>> CreateCourseAsync(CourseDto courseDto)
        {
            try
            {
                if (courseDto == null) return BadRequest();
                var existingCourse = await _courseRepository.SearchCourses(courseDto.Name);
                if (existingCourse.Count() > 0)
                {
                    ModelState.AddModelError("Name", $"Course with name - {courseDto.Name} already exists.");
                    return BadRequest(ModelState);
                }

                var newCourse = await _courseRepository.AddCourseAsync(courseDto);
                return CreatedAtAction(nameof(GetSingleCourse), new { id = newCourse.Id }, newCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating course. {ex.Message}");

            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("Edit/{Id:int}")]
        public async Task<ActionResult<Course>> EditCourseAsync(int Id, CourseDto courseDto)
        {
            try
            {
                if (courseDto == null) return BadRequest();
                var courseToUpdate = await _courseRepository.GetSingleCourseAsync(Id);
                if (courseToUpdate == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Course with ID - {Id} does not exist");
                }
                await _courseRepository.UpdateCourseAsync(Id, courseDto);
                var updatedCourse = await _courseRepository.GetSingleCourseAsync(Id);
                return Ok(updatedCourse);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating course. {ex.Message}");

            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{Id:int}")]
        public async Task<ActionResult> DeleteCourseAsync(int Id)
        {
            try
            {
                var courseToDelete = await _courseRepository.GetSingleCourseAsync(Id);
                if (courseToDelete == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Course with ID - {Id} does not exist");

                }
                await _courseRepository.DeleteCourseAsync(Id);
                return Ok($"{courseToDelete.Name} has been deleted successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting course from database. {ex.Message}");

            }
        }


    }
}
