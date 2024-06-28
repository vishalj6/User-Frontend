using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UsersProject.Middleware
{
    public static class EncryptionHelper
    {
        private static readonly string EncryptionKey = "your-encryption-key-here"; // Ensure this key is securely stored

        public static string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            using (Aes aesAlg = Aes.Create())
            {
                using (var key = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes("SaltValue")))
                {
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);
                }

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }

}
