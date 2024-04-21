namespace CourseManagementAPI.Security.Interfaces
{
    public interface IHasher
    {
        string EncryptPassword(string content);
        bool VerifyPassword(string password, string hashedPassword);
        bool SlowEquals(byte[] a, byte[] b);
    }
}
