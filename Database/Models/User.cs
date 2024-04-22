using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManagementAPI.Database.Models
{
    public class User : Common
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Role { get; set; }
        [ForeignKey("Email")]
        [JsonIgnore]
        public UserLogin UserLogin { get; set; }

        public User()
        {
            if (FirstName == null)
            {
                FirstName = "";
            }

            if (LastName == null)
            {
                LastName = "";
            }

            if (Gender == null)
            {
                Gender = "";
            }

            if (Email == null)
            {
                Email = "";
            }

            if (Username == null)
            {
                Username = "";
            }

            if (Role == null)
            {
                Role = "";
            }
        }
    }
}
