using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementAPI.Database.Models
{
    public class User : Common
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [ForeignKey("Email")]
        public UserLogin UserLogin { get; set; }
    }
}
