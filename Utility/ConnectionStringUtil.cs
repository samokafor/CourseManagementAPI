using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace CourseManagementAPI.Utility
{
    public class ConnectionStringUtil
    {
        private readonly IConfiguration config;
        public ConnectionStringUtil(IConfiguration configuration)
        {
            config = configuration;
        }
        public static string Encryptor(string connectionString, string encryptionKey, EncodingTypes? encoding = null)
        {
            var secretKey = new byte[] { };
            if (encoding == EncodingTypes.Base64String)
            {
                secretKey = Convert.FromBase64String(encryptionKey);
            }
            else
            {
                secretKey = Encoding.UTF8.GetBytes(encryptionKey);
            }

            var encryptionVector = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };

            var encryptor = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                BlockSize = 128,
                IV = encryptionVector,
                Key = secretKey
            };
            var plainBytes = Encoding.UTF8.GetBytes(connectionString);
            var encryptedBytes = encryptor.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decryptor(string request, string encryptionKey, EncodingTypes encoding)
        {
            var secretKey = new byte[] { };
            if(encoding == EncodingTypes.Base64String)
            {
                secretKey = Convert.FromBase64String(encryptionKey);
            }
            else
            {
                secretKey = Encoding.UTF8.GetBytes(encryptionKey);
            }

            byte[] vectorKey = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decryptor = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                BlockSize = 128,
                IV = vectorKey,
                Key = secretKey
            };
            var byteRequest = Convert.FromBase64String(request);
            var decryptedBytes = decryptor.CreateDecryptor().TransformFinalBlock(byteRequest, 0, byteRequest.Length);

            return Encoding.UTF8.GetString(decryptedBytes); 
        }

        public string GetConnectionString(string section)
        {
            var encryptedConnectionString = config.GetConnectionString(section);
            var connectionString = Decryptor(encryptedConnectionString, config["UtilitySettings:AESKey"], EncodingTypes.Base64String);
            return connectionString;
        }
    }
}
