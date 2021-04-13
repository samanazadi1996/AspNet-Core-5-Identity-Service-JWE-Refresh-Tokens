using Common;
using Microsoft.Extensions.Options;
using Services.CryptographyDomain.Abstraction;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Services.CryptographyDomain.Implementation
{
    public class DecryptService : IDecryptService
    {
        private readonly SiteSettings setting;

        public DecryptService(IOptionsSnapshot<SiteSettings> Setting)
        {
            setting = Setting.Value;
        }
        public string Decrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(setting.EncryptionSettings.Key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }

        }
    }
}
