using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;

namespace CourseManagementAPI.Security.Interfaces
{
    public interface IUserAuthentication
    {
        string GenerateToken(User user);
        User Authenticate(UserLoginDto userLogin);
    }
}
