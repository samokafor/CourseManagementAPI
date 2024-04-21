using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;

namespace CourseManagementAPI.Repositories.Interfaces
{
    public interface IInstructorRepository
    {
        public Task<Instructor> GetSingleInstructorAsync(int Id);
        public Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        public Task<IEnumerable<Instructor>> SearchInstructorsAsync(string searchTerm);
        public Task<Instructor> AddInstructorAsync(InstructorDto instructorDto);
        public Task<Instructor> UpdateInstructorAsync(int Id, InstructorDto instructorDto);
        public Task DeleteInstructorAsync(int Id);
    }
}
