using CourseManagementAPI.CommonProperties;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.Database.Models
{
    public class UserLogin : Common
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
