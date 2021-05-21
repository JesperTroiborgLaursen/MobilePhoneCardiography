using System;
using DTOs;
using MobilePhoneCardiography.Services.DataStore;
using NUnit.Framework;

namespace DataAccessLayer.Test.Unit
{
    public class Salt_Test_Unit
    {
        private Salt uut;
        [SetUp]
        public void Setup()
        {
            uut = new Salt();
        }

        [TestCase("W)ska5zX#8G:", 0)]
        [TestCase("gGNaNRcMBow7",1)]
        [TestCase("1DUr3/W(8ue:",2)]
        public void Salt_GetSalt_TrueSalt(string result, int nb)
        {
            Assert.That(uut.GetSalt(nb), Is.EqualTo(result));
        }

        [TestCase("W)ska5zX#8G:", 2)]
        [TestCase("gGNaNRcMBow7", 0)]
        [TestCase("1DUr3/W(8ue:", 1)]
        public void Salt_GetSalt_WrontSalt(string result, int nb)
        {
            Assert.That(uut.GetSalt(nb), !Is.EqualTo(result));
        }
    }
}