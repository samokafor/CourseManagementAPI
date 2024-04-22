using CourseManagementAPI.Database;
using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace CourseManagementAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _context;
        private TextInfo convert = CultureInfo.CurrentCulture.TextInfo;

        public CourseRepository(CourseDbContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourseAsync(CourseDto courseDto)
        {
            if (courseDto == null) return null;

            if (courseDto.InstructorId != null && (await _context.Instructors.FirstOrDefaultAsync(i => i.Id == courseDto.InstructorId)) == null)
            {
                throw new Exception($"No instructor exists with ID - {courseDto.InstructorId}");
            }


            var instructor = (string.IsNullOrEmpty(courseDto.InstructorId.ToString()) || courseDto.InstructorId != null)
                ? await _context.Instructors.FirstOrDefaultAsync(i => i.Id == courseDto.InstructorId)
                : null;


            var newCourse = new Course()
            {
                Name = convert.ToTitleCase(courseDto.Name.ToLower()),
                Description = convert.ToTitleCase(courseDto.Description.ToLower()),
                CourseDuration = courseDto.CourseDuration,
                InstructorId = instructor?.Id,
                InstructorName = instructor != null ? instructor.Name : "",
                StartDate = courseDto.StartDate,
                EndDate = courseDto.StartDate.AddDays(courseDto.CourseDuration),
                DateCreated = DateTime.Now,
            };
            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();

            return await GetSingleCourseAsync(newCourse.Id);
        }


        public async Task DeleteCourseAsync(int Id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetSingleCourseAsync(int Id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null) return null;

            return course;
        }

        public async Task<IEnumerable<Course>> SearchCourses(string searchTerm)
        {
            IQueryable<Course> query = _context.Courses;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm) || c.Description.Contains(searchTerm));
                return await query.ToListAsync();
            }
            throw new Exception("Search term cannot be empty.");
        }


        public async Task<Course> UpdateCourseAsync(int Id, CourseDto courseDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (courseDto == null) return null;

            if (courseDto.InstructorId != null && (await _context.Instructors.FirstOrDefaultAsync(i => i.Id == courseDto.InstructorId)) == null)
            {
                throw new Exception($"No instructor exists with ID - {courseDto.InstructorId}");
            }


            var instructor = (string.IsNullOrEmpty(courseDto.InstructorId.ToString()) || courseDto.InstructorId != null)
                ? await _context.Instructors.FirstOrDefaultAsync(i => i.Id == courseDto.InstructorId)
                : null;

            if (course != null)
            {
                course.Name = convert.ToTitleCase(courseDto.Name.ToLower());
                course.Description = convert.ToTitleCase(courseDto.Description.ToLower());
                course.CourseDuration = courseDto.CourseDuration;
                course.InstructorId = instructor?.Id;
                course.InstructorName = instructor != null ? instructor.Name : "";
                course.StartDate = courseDto.StartDate;
                course.EndDate = courseDto.StartDate.AddDays(courseDto.CourseDuration);
                course.DateModified = DateTime.Now;

                await _context.SaveChangesAsync();
                return await GetSingleCourseAsync(course.Id);
            }
            return null;


        }
    }
}
