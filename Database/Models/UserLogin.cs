using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.Database.Models
{
    public class UserLogin : Common
    {
        [Key]

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public UserLogin()
        {
            if (Email == null)
            {
                Email = "";
            }
            
            if (Password == null)
            {
                Password = "";
            }
        }
    }

}
