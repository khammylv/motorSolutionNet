using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Services
{
    public class EncryptionHelper
    {
        private static readonly string keyBase64 = ConfigManager.GetConfigValue("KEY"); 
        private static readonly string ivBase64 = ConfigManager.GetConfigValue("IV");


        public static string Encrypt(string plainText)
        {
            byte[] key = Convert.FromBase64String(keyBase64); 
            byte[] iv = Convert.FromBase64String(ivBase64);  
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(cipherBytes);
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] key = Convert.FromBase64String(keyBase64);
            byte[] iv = Convert.FromBase64String(ivBase64);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(plainBytes);
            }
        }
    }
}