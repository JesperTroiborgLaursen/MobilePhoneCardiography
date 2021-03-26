using System;
using System.IO;
using DTOs;
using NUnit.Framework;

namespace DataAccessLayer.Test.Unit
{
    public class CosmosDBService_Test_Unit
    {
        private ICosmosDBService uut;

        [SetUp]
        public void Setup()
        {
            //    static byte[] listBoll = new byte[] { 1, 0, 1, 0, 0, 1 };
            //     private static Stream stream = new MemoryStream(listBoll);
            //     private Measurement measurement = new Measurement() { Id = 101001, HealthProfID = "testID = 1,0,1,0,0,1", HeartSound = stream, StartTime = DateTime.Now };
        }

    
        [TestCase(EnumDatabase.Measurement, "Measurement")]
        [TestCase(EnumDatabase.Patient, "Patient")]
        [TestCase(EnumDatabase.Professionel, "ProfessionelUser")]
        public void DatabaseChoiceInjection_CollectionNameSaved_TestIsCorrect(EnumDatabase choice, string result)
        {
            uut = new CosmosDBService(choice, DateTime.Now);

            Assert.That(uut.DatabaseChoice(choice), Is.EqualTo(result));
        }

        [TestCase(EnumDatabase.Measurement, "ERROR")]
        [TestCase(EnumDatabase.Patient, "ProfessninelUser")]
        [TestCase(EnumDatabase.Professionel, "Patient")]
        public void DatabaseChoiceInjection_CollectionNameSaved_TestIsFalse(EnumDatabase choice, string result)
        {

            uut = new CosmosDBService(choice, DateTime.Now);
            Assert.That(uut.DatabaseChoice(choice), !Is.EqualTo(result));
        }


    }
}