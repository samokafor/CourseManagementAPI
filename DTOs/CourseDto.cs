namespace CourseManagementAPI.DTOs
{
    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CourseDuration { get; set; } //This should be in Days
        public int? InstructorId { get; set; }
        public DateTime StartDate { get; set; }

        public CourseDto()
        {
            if (Name == null)
            {
                Name = "";
            }
            if (Description == null)
            {
                Description = "";
            }
        }
    }
}
