using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.Database.Models
{
    public class Instructor : Common
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Institution { get; set; }
        [Required]
        public double Rating { get; set; }
        public List<Course> Courses { get; set; }

        public Instructor()
        {
            if(Name == null)
            {
                Name = "";
            }
            if(Gender == null)
            {
                Gender = "";
            }
            if(Institution == null)
            {
                Institution = "";
            }
            if(Courses == null)
            {
                Courses = new List<Course>();
            }
        }
    }
}
