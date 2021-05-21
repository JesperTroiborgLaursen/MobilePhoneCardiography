using System;
using System.Security.Cryptography;
using System.Text;

namespace MobilePhoneCardiography.Services.DataStore
{
    public class SecurityCyptography : ISecurity
    {
        public string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
} 