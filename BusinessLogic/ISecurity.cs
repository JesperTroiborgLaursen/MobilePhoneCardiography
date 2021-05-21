using System.Security.Cryptography;

namespace MobilePhoneCardiography.Services.DataStore
{
    public interface ISecurity
    {
        public string GetHash(HashAlgorithm hashAlgorithm, string input);

    }
}