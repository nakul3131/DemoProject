using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;

namespace DemoProject.Services.Concrete.Security
{
    public class EFCryptoAlgorithmRepository : ICryptoAlgorithmRepository
    {
        public string Encrypt(string _input, string _key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(_input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(_key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string _input, string _key)
        {
            byte[] inputArray = Convert.FromBase64String(_input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = Encoding.UTF8.GetBytes(_key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        public string EncryptFileNameByAsymmetricKey()
        {
            // Add Current Date To Attached File Name For Encryption 
            string currentDateTime = DateTime.Now.ToString("ddMMyyyyssfff");

            // Encrypt File Name And Removing Special Characters From Name For Valid File Name.
            return Regex.Replace(Encrypt(currentDateTime, StringLiteralValue.FileName), @"[^0-9a-zA-Z]+", "");
        }
    }
}
