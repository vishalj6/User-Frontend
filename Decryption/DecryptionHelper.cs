using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UsersProject.Middleware
{
    public static class DecryptionHelper
    {
        private static readonly string EncryptionKey = "your-encryption-key-here"; // Ensure this key is securely stored
        public static string DecryptString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            using (Aes aesAlg = Aes.Create())
            {
                // Derive key and IV
                using (var key = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes("SaltValue")))
                {
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);
                }

                // Decrypt the data
                using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
