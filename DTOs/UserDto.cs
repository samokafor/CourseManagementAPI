using CourseManagementAPI.Database.Models;
using System.Text.Json.Serialization;

namespace CourseManagementAPI.DTOs
{
    public class UserDto : CommonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public UserLogin UserLogin { get; set; }
    }
}
