namespace CourseManagementAPI.Security.Interfaces
{
    public interface IPasswordHasher
    {
        string EncryptPassword(string password);
        string DecryptPassword(string base64EncodeData);
    }
}
