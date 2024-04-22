using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManagementAPI.Database.Models
{
    public class Course : Common
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double CourseDuration { get; set; }//This should be in Days
        [Required]
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
        [ForeignKey("InstructorId")]
        [JsonIgnore]
        public Instructor? Instructor { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public Course()
        {
            if(Name == null)
            {
                Name = "";
            }
            if(Description == null)
            {
                Description = "";
            }
        }

    }
}
