using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using DemoProject.Domain.CustomEntities.CBS;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.Utility.CyberSecurity
{
    public class VangheeCoreTransactionCrypto
    {
        private readonly ICoreTransactionDetailRepository coreTransactionDetailRepository;

        private static readonly string IV = "94A149SH952A88DB";
        private static readonly string KEY = "9132SHR95ABAABF7D3A0B4BE8B2D6GBE";

        public VangheeCoreTransactionCrypto()
        {
            coreTransactionDetailRepository = DependencyResolver.Current.GetService<ICoreTransactionDetailRepository>();

            CBSAccountCredential cBSAccountCredential = new CBSAccountCredential();

            cBSAccountCredential = coreTransactionDetailRepository.GetCBSAccountCredentials("Vanghee");

            // Get AESKey
            //IV = cBSAccountCredential.SecretKeyForEncryption;
            //KEY = cBSAccountCredential.AdvancedEncryptionStandardKey;
        }

        public static string EncryptAndEncode(string raw)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                string encrypted = Convert.ToBase64String(output);

                return encrypted;
            }
        }

        public static string DecodeAndDecrypt(string encryptedResponseValue)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var d = GetCryptoTransform(csp, false);
                byte[] output1 = Convert.FromBase64String(encryptedResponseValue);
                byte[] decryptedOutput = d.TransformFinalBlock(output1, 0, output1.Length);
                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        public static string CreateChecksum(string rawData)
        {
            // Create a SHA256 
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array 
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string 
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString().ToUpper();           
            }
        }


        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            csp.KeySize = 256;

            csp.IV = Encoding.UTF8.GetBytes(IV);
            csp.Key = Encoding.UTF8.GetBytes(KEY);
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }

        static void Main(string[] args)
        {
            string encryptMe;
            string encrypted;
            string decryptMe;
            string decrypted;
            string checkSum;

            encryptMe = "{\"custId\":\"encrypted Test \"}";
            //2. Calculate SHA-256 checksum
            checkSum = CreateChecksum(encryptMe);
            //3. Encrypt the JSON string data using AES encryption key and the IV
            encrypted = EncryptAndEncode(encryptMe);
            decryptMe = encrypted;
            decrypted = DecodeAndDecrypt(decryptMe);
           
        }


    }
}
