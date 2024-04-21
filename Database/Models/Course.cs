using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManagementAPI.Database.Models
{
    public class Course : Common
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CourseDuration { get; set; } //This should be in Days
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
        [ForeignKey("InstructorId")]
        [JsonIgnore]
        public Instructor? Instructor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
