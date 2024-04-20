namespace CourseManagementAPI.Security.Interfaces
{
    public interface IPasswordHasher
    {
        string EncryptPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        bool SlowEquals(byte[] a, byte[] b);
    }
}
