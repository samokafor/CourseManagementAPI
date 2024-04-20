using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;

namespace CourseManagementAPI.Security.Interfaces
{
    public interface IUserAuthentication
    {
        string GenerateToken(UserDto user);
        UserDto Authenticate(UserLoginDto userLogin);

        bool IsValidPassword(string password);

        string PasswordRequiremnts();
    }
}
