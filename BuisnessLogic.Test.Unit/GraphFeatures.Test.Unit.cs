using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;
using NUnit.Framework;

namespace BuisnessLogic.Test.Unit
{
    public class GraphFeatures
    {
        private IGraphFeatures UUT;
        [SetUp]
        public void Setup()
        {
            UUT = new BusinessLogic.GraphFeatures();
        }

        [Test]
        public void DownSample1000Samples_ReturnedArrayCointains5Samples()
        {
            //ARRANGE
            byte[] testBytesArray = new byte[1000];
            byte testByte = 12; 
            for (int i = 0; i < testBytesArray.Length; i++)
            {
                testBytesArray[i] = testByte;
            }

            //ACT
            var result = UUT.DownSample(testBytesArray);

            //ASSERT
            Assert.That(result.Length, Is.EqualTo(4));
        }

        [Test]
        public void DownSample0Samples_ReturnedArrayCointains0Samples()
        {
            //ARRANGE
            byte[] testBytesArray = new byte[0];
            byte testByte = 12;
            for (int i = 0; i < testBytesArray.Length; i++)
            {
                testBytesArray[i] = testByte;
            }

            //ACT
            var result = UUT.DownSample(testBytesArray);

            //ASSERT
            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void DownSample1000Samples_TwoTimes_ReturnedArrayCointains5Samples()
        {
            //ARRANGE
            byte[] testBytesArray1 = new byte[1000];
            byte[] testBytesArray2 = new byte[1000];
            byte testByte = 12;
            for (int i = 0; i < testBytesArray1.Length; i++)
            {
                testBytesArray1[i] = testByte;
                testBytesArray2[i] = testByte;
            }

            //ACT
            var result1 = UUT.DownSample(testBytesArray1);
            var result2 = UUT.DownSample(testBytesArray2);

            //ASSERT
            Assert.Multiple(() =>
            {
                Assert.That(result1.Length, Is.EqualTo(4));
                Assert.That(result2.Length, Is.EqualTo(4));
            });
        }
    }
}
