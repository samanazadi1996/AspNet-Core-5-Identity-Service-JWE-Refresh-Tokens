using Common;
using Microsoft.Extensions.Options;
using Services.CryptographyDomain.Abstraction;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Services.CryptographyDomain.Implementation
{
    public class EncryptService : IEncryptService
    {
        private readonly SiteSettings setting;

        public EncryptService(IOptionsSnapshot<SiteSettings> Setting)
        {
            setting = Setting.Value;
        }
        public string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;


            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(setting.EncryptionSettings.Key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);

        }
    }
}
