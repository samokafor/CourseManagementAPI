using CourseManagementAPI.Database.Models;

namespace CourseManagementAPI.DTOs
{
    public class InstructorDto : CommonDto
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public List<Course> Courses { get; set; }
    }
}
