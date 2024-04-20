using CourseManagementAPI.CommonProperties;

namespace CourseManagementAPI.Database.Models
{
    public class Instructor : Common
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Institution { get; set; }
        public int Rating { get; set; }
        public List<Course> Courses { get; set; } /*= new List<Course>()
        {
            new Course()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                Name = "PHY300",
                Description = "Analytical Physics",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(6)
            },
            new Course()
            {
                Id = 2,
                DateCreated = DateTime.Now,
                Name = "MTH200",
                Description = "Advanced Calculus",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(7)
            },
            new Course()
            {
                Id = 3,
                DateCreated = DateTime.Now,
                Name = "CHM400",
                Description = "Organic Chemistry",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(8)
            },
            new Course()
            {
                Id = 4,
                DateCreated = DateTime.Now,
                Name = "BIO150",
                Description = "Marine Biology",
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(9)
            },
            new Course()
            {
                Id = 5,
                DateCreated = DateTime.Now,
                Name = "CSC500",
                Description = "Data Structures and Algorithms",
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(10)
            }
        };*/
    }
}
