using System;
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