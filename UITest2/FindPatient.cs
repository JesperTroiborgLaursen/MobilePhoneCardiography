using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest2
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class FindPatient
    {
        IApp app;
        Platform platform;

        public FindPatient(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            app.Tap("PrivateUserLoginButton");
            app.Tap(c => c.Marked("Patients"));


        }

        [Test]
        public void Entering1234567890AsSocSec_PressingFindPatient_PatientDataForJohnDoeLoads()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "1234567890");
            app.PressEnter();
            
            //Act
            app.Tap("FindPatientButton");
            var socSec = app.Query("SocSec").FirstOrDefault().Text;
            var firstName = app.Query("FirstName").FirstOrDefault().Text;
            var lastName = app.Query("LastName").FirstOrDefault().Text;

            //Assert
            Assert.That(socSec, Is.EqualTo("1234567890"));
            Assert.That(firstName, Is.EqualTo("John"));
            Assert.That(lastName, Is.EqualTo("Doe"));
        }


        [Test]
        public void Entering2234567890AsSocSec_PressingFindPatient_PatientDataForJimSmithLoads()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "2234567890");
            app.PressEnter();
            
            //Act
            app.Tap("FindPatientButton");
            var socSec = app.Query("SocSec").FirstOrDefault().Text;
            var firstName = app.Query("FirstName").FirstOrDefault().Text;
            var lastName = app.Query("LastName").FirstOrDefault().Text;

            //Assert
            Assert.That(socSec, Is.EqualTo("2234567890"));
            Assert.That(firstName, Is.EqualTo("Jim"));
            Assert.That(lastName, Is.EqualTo("Smith"));
        }

        [Test]
        public void LoadPatientJimSmith_TapCancel_AllEntriesClears()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "2234567890");
            app.PressEnter();
            app.Tap("FindPatientButton");
            
            
            //Act
            app.Tap("CancelButton");
            var socSec = app.Query("SocSec").FirstOrDefault().Text;
            var socSecSearch = app.Query("SocSecSearch").FirstOrDefault().Text;
            var firstName = app.Query("FirstName").FirstOrDefault().Text;
            var lastName = app.Query("LastName").FirstOrDefault().Text;


            //Assert
            Assert.That(socSec, Is.EqualTo(""));
            Assert.That(socSecSearch, Is.EqualTo(""));
            Assert.That(firstName, Is.EqualTo(""));
            Assert.That(lastName, Is.EqualTo(""));
        }
    }
}
