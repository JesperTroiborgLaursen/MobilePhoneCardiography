using System.Security.Cryptography;
using MobilePhoneCardiography.Services.DataStore;
using NSubstitute;
using NUnit.Framework;

namespace DataAccessLayer.Test.Unit
{
    public class SecurityCyptography_Test_Unit
    {
        private SecurityCyptography uut;

        [SetUp]
        public void Setup()
        {
            uut = new SecurityCyptography();
        }

        [TestCase("123456-1234", "13dbda484804b782740cc36f5f220b59dc152dd28bf084e84c24582907aa82b4", true)]
        [TestCase("123456-7890", "6500ef5bcd77cf851c726609bf08f60eed665edb8c6a822d099179889fe82bb1", true)]
        [TestCase("'@>e>+o'U0MD", "594c0ce27bcb367e1b97fa6f903105f081cce01e055307eb6507d14f59b914bf", true)]
        [TestCase("123456-12345", "13dbda484804b782740cc36f5f220b59dc152dd28bf084e84c24582907aa82b4", false)]
        [TestCase("123456-78901", "6500ef5bcd77cf851c726609bf08f60eed665edb8c6a822d099179889fe82bb1", false)]
        [TestCase("'@>e>+o'U0MDU", "594c0ce27bcb367e1b97fa6f903105f081cce01e055307eb6507d14f59b914bf", false)]
        public void SecurityCypto_GetHash_TrueOrFalseHash(string code, string hash, bool result)
        {
            SHA256 sha256 = SHA256.Create();
            Assert.That(uut.GetHash(sha256, code)==hash, Is.EqualTo(result) );
        }


    }
}