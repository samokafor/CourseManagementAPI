using CourseManagementAPI.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs
{
    public class InstructorDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Institution { get; set; }
        public double Rating { get; set; }

        public InstructorDto()
            {
                if (Name == null)
                {
                    Name = "";
                }
                if (Gender == null)
                {
                    Gender = "";
                }
                if (Institution == null)
                {
                    Institution = "";
                }
            }
    }
}
