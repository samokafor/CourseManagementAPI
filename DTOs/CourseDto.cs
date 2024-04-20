using CourseManagementAPI.Database.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementAPI.DTOs
{
    public class CourseDto : CommonDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CourseDuration { get; set; } //This should be in Days
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
