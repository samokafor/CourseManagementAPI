using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManagementAPI.Database.Models
{
    public class User : Common
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        [ForeignKey("Email")]
        [JsonIgnore]
        public UserLogin UserLogin { get; set; }
    }
}
