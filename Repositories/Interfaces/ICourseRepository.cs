using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;

namespace CourseManagementAPI.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> AddCourseAsync(CourseDto courseDto);
        Task DeleteCourseAsync(int Id);
        Task<Course> GetSingleCourseAsync(int Id);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> UpdateCourseAsync(int Id, CourseDto courseDto);
        Task<IEnumerable<Course>> SearchCourses(string searchTerm);
        
    }
}
