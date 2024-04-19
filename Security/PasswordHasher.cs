
using CourseManagementAPI.Security.Interfaces;
using System.Text;

namespace CourseManagementAPI.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public static string Key = "!@#$%^23?Hf&e.*{>";

        public PasswordHasher()
        {
        }

        public string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public string DecryptPassword(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
