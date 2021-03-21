using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using MobilePhoneCardiography.Models.Json;
using MobilePhoneCardiography.Services.DataStore;
using NSubstitute;
using NUnit.Framework;

namespace DataAccessLayer.Test.Unit
{
    public class ControllerDatabase_Test_Unit
    {
        private IControllerDatabase uut;
        private ICosmosDBService cosmosDb;
        private IPatient patientCompare;
        private IUser userCompare;

        #region NewUser
        //var stubUser = new User()
        //{
        //    Username = "test", Id = "test", Password = "test", SSN = "test"
        //};


        #endregion

        [SetUp]
        public void Setup()
        {

        }

        #region ValidatePatient
        [Test]
        public async Task ValidatePatient_StubReturnRealPatient_TestIsCorrect()
        {
            #region SetUp
            cosmosDb = Substitute.For<ICosmosDBService>();
            patientCompare = new Patient()
            {
                SocSec = "1234561234",
                Id = "test"
            };

            #endregion
            
            cosmosDb.GetSSN(patientCompare).Returns(new List<JsonPatientId>
            {
                new JsonPatientId()
                {
                    PatientId = "1234561234"
                }
            });
            IPatient fakeUser = new Patient();
            uut = new ControllerDatabase(cosmosDb);
            var svar = await uut.ValidatePatient(patientCompare);

            //Check Validate
            Assert.That(svar, Is.EqualTo(true));

            //Check revieved calls:
            await cosmosDb.Received(1).GetSSN(patientCompare);
            await cosmosDb.Received(0).GetSSN(fakeUser);
            await cosmosDb.DidNotReceive().GetSSN(fakeUser);


        }

        [TestCase("1234561234",true)]
        [TestCase("1134561234", false)]
        [TestCase("LongStupidString", false)]
        public async Task ValidatePatients_StubReturnRealPatient_TestCaseIsCorrect(string SocSec, bool result)
        {
            #region SetUp
            cosmosDb = Substitute.For<ICosmosDBService>();
            patientCompare = new Patient()
            {
                SocSec = "1234561234",
                Id = "test"
            };

            #endregion

            cosmosDb.GetSSN(patientCompare).Returns(new List<JsonPatientId>
            {
                new JsonPatientId()
                {
                    PatientId = SocSec
                }
            });
            IPatient fakeUser = new Patient();
            uut = new ControllerDatabase(cosmosDb);
            var svar = await uut.ValidatePatient(patientCompare);

            //Check Validate
            Assert.That(svar, Is.EqualTo(result));

            //Check revieved calls:
            await cosmosDb.Received(1).GetSSN(patientCompare);
            await cosmosDb.Received(0).GetSSN(fakeUser);
            await cosmosDb.DidNotReceive().GetSSN(fakeUser);


        }

        #endregion
        #region ValidateLogin

        [Test]
        public async Task ValidateLogin_StubreturnRealUser_TestIsCorrect()
        {
            #region SetUp
            cosmosDb = Substitute.For<ICosmosDBService>();

            
            userCompare = new User()
            {
                Password = "test",
                Username = "test"
            };

            #endregion

            cosmosDb.GetLogin(userCompare).Returns(new List<JsonProfessionalUser>
            {
                new JsonProfessionalUser()
                {
                    HealthProfID = "test",UserPW = "test"
                }
            });
            IUser fakeUser = new User();
            uut = new ControllerDatabase(cosmosDb);
            var svar = await uut.ValidateLogin(userCompare);

            //Check Validate
            Assert.That(svar, Is.EqualTo(true));

            //Check revieved calls:
            await cosmosDb.Received(1).GetLogin(userCompare);
            await cosmosDb.DidNotReceive().GetLogin(fakeUser);

        }

        [TestCase("test", "test", true)]
        [TestCase("test", "ERROR", false)]
        [TestCase("Error", "test", false)]
        [TestCase("Test", "Test", false)]
        [TestCase("TEST", "TEST", false)]
        [TestCase("test1", "test2", false)]
        public async Task ValidateLoginTestCase_StubWithCosmosDb_TestCases(string id, string pw, bool result)
        {
            #region SetUp
            cosmosDb = Substitute.For<ICosmosDBService>();

            
            userCompare = new User()
            {
                Password = "test",
                Username = "test"
            };

            #endregion

            cosmosDb.GetLogin(userCompare).Returns(new List<JsonProfessionalUser>
            {
                new JsonProfessionalUser()
                {
                    HealthProfID = id,
                    UserPW = pw
                }
            });
            IUser fakeUser = new User();
            uut = new ControllerDatabase(cosmosDb);
            var svar = await uut.ValidateLogin(userCompare);


            //Check revieved calls:
            await cosmosDb.Received(1).GetLogin(userCompare);
            await cosmosDb.DidNotReceive().GetLogin(fakeUser);

            // Chech ValidateLogin
            Assert.That(svar, Is.EqualTo(result));


        }


        #endregion

    }
}