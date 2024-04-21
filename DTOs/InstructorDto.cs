using CourseManagementAPI.Database.Models;

namespace CourseManagementAPI.DTOs
{
    public class InstructorDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Institution { get; set; }
        public double Rating { get; set; }
    }
}
