using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest2
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class UTFindPatient
    {
        IApp app;
        Platform platform;

        public UTFindPatient(Platform platform)
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
        public void FindPatientButton_EnterTextInSocSecSearch_FindPatientButtonIsEnabled()
        {

            //Arrange
            app.Tap("SocSecSearch");

            //Act
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();
            var findPatientButtonEnabled = app.Query(c => c.Marked("FindPatientButton")).FirstOrDefault().Enabled;

            //Assert
            Assert.That(findPatientButtonEnabled, Is.True);
        }

        [Test]
        public void FindPatientButton_EnterAndThenRemoveTextInSocSearch_FindPatientButtonIsDisabled()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();
            app.Tap("SocSecSearch");
            app.ClearText();
            app.PressEnter();

            //Act
            var findPatientButtonEnabled = app.Query(c => c.Marked("FindPatientButton")).FirstOrDefault().Enabled;

            //Assert
            Assert.That(findPatientButtonEnabled, Is.False);
        }
        
        [Test]
        public void FindPatientButton_LoadPatient_FindButtonNotVisible()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();


            //Act
            app.Tap("FindPatientButton");

            //Assert
            try
            {
                app.WaitForNoElement("FindPatientButton");
            }
            catch (Exception e)
            {
                Assert.Fail();
                throw;
            }
            Assert.Pass();
        }

        [Test]
        public void ConfirmButton_LoadPatient_ConfirmButtonIsVisible()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();


            //Act
            app.Tap("FindPatientButton");

            //Assert
            try
            {
                app.WaitForElement("ConfirmButton");
            }
            catch (Exception e)
            {
                Assert.Fail();
                throw;
            }
            Assert.Pass();
        }

        [Test]
        public void ConfirmButton_LoadPatientPressConfirm_ViewChangesToRecordings()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();
            app.Tap("FindPatientButton");
            app.Tap("ConfirmButton");

            var tabElement = app.WaitForElement("RecordingsList");

            //Assert
            Assert.That(tabElement, Is.Not.Null);
        }




        [Test]
        public void CancelButton_LoadPatient_CancelButtonIsVisible()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();


            //Act
            app.Tap("FindPatientButton");

            //Assert
            try
            {
                app.WaitForElement("CancelButton");
            }
            catch (Exception e)
            {
                Assert.Fail();
                throw;
            }
            Assert.Pass();
        }


        [Test]
        public void FindPatientButton_Entering1234567890AsSocSecPressingFindPatient_PatientDataForAndersSalty()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();

            //Act
            app.Tap("FindPatientButton");
            var socSec = app.Query("SocSec").FirstOrDefault().Text;
            var firstName = app.Query("FirstName").FirstOrDefault().Text;
            var lastName = app.Query("LastName").FirstOrDefault().Text;
            Thread.Sleep(200);

            //Assert
            Assert.That(socSec, Is.EqualTo("123456-1234"));
            Assert.That(firstName, Is.EqualTo("Anders"));
            Assert.That(lastName, Is.EqualTo("Salty"));
        }


        [Test]
        public void CancelButton_LoadPatientJimSmithTapCancel_AllEntriesClears()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();
            app.Tap("FindPatientButton");
            
            
            //Act
            app.Tap("CancelButton");
            var socSec = app.Query("SocSec").FirstOrDefault().Text;
            var socSecSearch = app.Query("SocSecSearch").FirstOrDefault().Text;
            var firstName = app.Query("FirstName").FirstOrDefault().Text;
            var lastName = app.Query("LastName").FirstOrDefault().Text;
            //var findPatientButtonVisible = app.Query(x => x.Marked("FindPatientButton").Invoke("getIsVisible"))[0];
            var findPatientButtonEnabled = app.Query(c => c.Marked("FindPatientButton")).FirstOrDefault().Enabled;



            //Assert
            Assert.That(socSec, Is.EqualTo(""));
            Assert.That(socSecSearch, Is.EqualTo(""));
            Assert.That(firstName, Is.EqualTo(""));
            Assert.That(lastName, Is.EqualTo(""));
            Assert.That(findPatientButtonEnabled, Is.False);
            //Assert.That(findPatientButtonVisible, Is.True);
        }


        [Test]
        public void FindPatientButton_LoadPatient_ConsentLabelIsVisible()
        {

            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();
            app.Tap("FindPatientButton");

            //Act
            var consentLabelVisible = app.WaitForElement("ConsentLabel").First().Text;

            //Assert
            Assert.That(consentLabelVisible, Is.EqualTo("Please confirm with the patient if this information is correct, and if he/she consent with the usage of his/hers data"));
        }


        [Test]
        public void ConfirmButton_LoadPatientAndPressConfirm_RecordingsTabOpened()
        {
            //Arrange
            app.Tap("SocSecSearch");
            app.EnterText("SocSecSearch", "123456-1234");
            app.PressEnter();
            app.Tap("FindPatientButton");

            //Act
            app.Tap("ConfirmButton");
            var tabElement = app.WaitForElement("RecordingsList");

            //Assert
            Assert.That(tabElement, Is.Not.Null);
        }




    }
}
