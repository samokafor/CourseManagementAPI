namespace CourseManagementAPI.DTOs
{
    public class UserLoginDto : CommonDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
