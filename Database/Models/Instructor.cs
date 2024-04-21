using CourseManagementAPI.CommonProperties;

namespace CourseManagementAPI.Database.Models
{
    public class Instructor : Common
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Institution { get; set; }
        public double Rating { get; set; }
        public List<Course> Courses { get; set; } 
    }
}
