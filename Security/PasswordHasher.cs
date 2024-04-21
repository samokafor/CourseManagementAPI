using CourseManagementAPI.Security.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher : IPasswordHasher
{
    private static readonly byte[] Salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

    public string EncryptPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hash);
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            return false;

        byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(32); 

            // Compare the hashed password with the calculated hash
            return SlowEquals(hash, hashedPasswordBytes);
        }
    }

    public bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff = (uint)a.Length ^ (uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
        {
            diff |= (uint)(a[i] ^ b[i]);
        }
        return diff == 0;
    }
}
