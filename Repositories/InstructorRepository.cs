using CourseManagementAPI.Database;
using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CourseManagementAPI.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly CourseDbContext _context;
        private TextInfo convert = CultureInfo.CurrentCulture.TextInfo;

        public InstructorRepository(CourseDbContext context)
        {
            _context = context;
        }
        public async Task<Instructor> AddInstructorAsync(InstructorDto instructorDto)
        {
            if (instructorDto == null) return null;

            var newInstructor = new Instructor()
            {
                Name = convert.ToTitleCase(instructorDto.Name.ToLower()),
                Gender = convert.ToTitleCase(instructorDto.Gender.ToLower()),
                Institution = convert.ToTitleCase(instructorDto.Institution.ToLower()),
                Rating = instructorDto.Rating,
                DateCreated = DateTime.Now,
            };
            await _context.Instructors.AddAsync(newInstructor);
            await _context.SaveChangesAsync();

            return await GetSingleInstructorAsync(newInstructor.Id);
        }

        public async Task DeleteInstructorAsync(int Id)
        {
            var instructor = await GetSingleInstructorAsync(Id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            var instructors = await _context.Instructors.Include(i => i.Courses).ToListAsync();
            return instructors;
        }

        public async Task<Instructor> GetSingleInstructorAsync(int Id)
        {
            var instructor = await _context.Instructors
                          .Include(i => i.Courses)
                          .FirstOrDefaultAsync(i => i.Id == Id);
            return instructor;
        }

        public async Task<IEnumerable<Instructor>> SearchInstructorsAsync(string searchTerm)
        {
            IQueryable<Instructor> query = _context.Instructors.Include(i => i.Courses);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(i => i.Name.Contains(searchTerm) || i.Institution.Contains(searchTerm));
                return await query.ToListAsync();
            }
            throw new Exception("Search term cannot be empty.");

        }

        public async Task<Instructor> UpdateInstructorAsync(int Id, InstructorDto instructorDto)
        {
            if (instructorDto != null)
            {
                var instructor = await GetSingleInstructorAsync(Id);
                instructor.Name = convert.ToTitleCase(instructorDto.Name.ToLower());
                instructor.Gender = convert.ToTitleCase(instructorDto.Gender.ToLower());
                instructor.Institution = convert.ToTitleCase(instructorDto.Institution.ToLower());
                instructor.Rating = instructorDto.Rating;
                instructor.DateModified = DateTime.Now;

                await _context.SaveChangesAsync();
                return await GetSingleInstructorAsync(Id);
            }
            return null;
        }
    }
}
