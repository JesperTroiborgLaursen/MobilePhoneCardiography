using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOs;
using MobilePhoneCardiography.Models.Json;
using MobilePhoneCardiography.Services.DataStore;
using NSubstitute;
using NUnit.Framework;

namespace BuisnessLogic.Test.Unit
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
                    //todo sha256
                    PatientId = "339cefe8868f485d4c707f69a090ad5c5333077f7c6daae7a8c446a91e2c8440"
                }
            });
            IPatient fakeUser = new Patient();
            uut = new ControllerDatabase(cosmosDb);
            var svar = await uut.ValidatePatient(patientCompare);

            //Check Validate
            Assert.That(svar, Is.EqualTo(true));

            //Check received calls:
            await cosmosDb.Received(1).GetSSN(patientCompare);
            await cosmosDb.Received(0).GetSSN(fakeUser);
            await cosmosDb.DidNotReceive().GetSSN(fakeUser);


        }

        [TestCase("339cefe8868f485d4c707f69a090ad5c5333077f7c6daae7a8c446a91e2c8440", true)]
        [TestCase("462ddb9fa125fgac01fe132e057295c3b8fd1946f394b12c382ec4ab43b25cf5", false)]
        [TestCase("462ddb9fa125fdac01fe132e057295c3b8fdl946f394b12c382ec4ab43b25cf5", false)]
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
                    //todo sha256
                    HealthProfID = "test",UserPW = "8d6c370337db4269e937dc6126e4b94e7a8dc3e2e8cdb82213626ab5826b1bf0"
                }
            });
            IUser fakeUser = new User();
            uut = new ControllerDatabase(cosmosDb);
            var svar = await uut.ValidateLogin(userCompare);

            //Check Validate
            Assert.That(svar, Is.EqualTo(true));

            //Check revieved calls:
         
            await cosmosDb.DidNotReceive().GetLogin(fakeUser);

        }


        [TestCase("test",/*todo sha256*/ "8d6c370337db4269e937dc6126e4b94e7a8dc3e2e8cdb82213626ab5826b1bf0", true)]
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
            
            await cosmosDb.DidNotReceive().GetLogin(fakeUser);

            // Chech ValidateLogin
            Assert.That(svar, Is.EqualTo(result));


        }


        #endregion

    }
}