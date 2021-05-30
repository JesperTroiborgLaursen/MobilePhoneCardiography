using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace UITest2
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class UTLoginSPView
    {

        IApp app;
        Platform platform;

        public UTLoginSPView(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            app.Tap("HealthUserLoginButton");
        }



        [Test]
        public void LoginButton_EnterNoUsernameAndNOPWPressLoginButton_LoginButtonNotEnabled()
        {

            //Arrange
            app.Tap("UsernameEntry");

            //Act
            app.EnterText("UsernameEntry", "");
            app.PressEnter();
            app.Tap("PasswordEntry");
            app.EnterText("PasswordEntry", "");
            app.PressEnter();

            var LoginButtonEnabled = app.Query(c => c.Marked("LoginButton")).FirstOrDefault().Enabled;

            //Assert
            Assert.That(LoginButtonEnabled, Is.False);
        }

        [Test]
        public void LoginButton_EnterUsernametestAndPWtestPressLoginButton_LoginButtonEnabled()
        {

            //Arrange
            app.Tap("UsernameEntry");

            //Act
            app.EnterText("UsernameEntry", "test");
            app.PressEnter();
            app.Tap("PasswordEntry");
            app.EnterText("PasswordEntry", "test");
            app.PressEnter();

            var LoginButtonEnabled = app.Query(c => c.Marked("LoginButton")).FirstOrDefault().Enabled;

            //Assert
            Assert.That(LoginButtonEnabled, Is.True);
        }


        [Test]
        public void LoginButton_EnterUsernametestAndPWtestPressLoginButton_RecordingsTabOpens()
        {

            //Arrange
            app.Tap("UsernameEntry");

            //Act
            app.EnterText("UsernameEntry", "test");
            app.PressEnter();
            app.Tap("PasswordEntry");
            app.EnterText("PasswordEntry", "test");

            app.Tap("LoginButton");

            var loginResult = app.WaitForElement("RecordingsList");

            //Assert
            Assert.That(loginResult, Is.Not.Null);
        }

        [Test]
        public void LoginButton_EnterUsernameWrongAndPWtestPressLoginButton_WrongLabelShows()
        {

            //Arrange
            app.Tap("UsernameEntry");

            //Act
            app.EnterText("UsernameEntry", "Wrong");
            app.PressEnter();
            app.Tap("PasswordEntry");
            app.EnterText("PasswordEntry", "test");
            app.PressEnter();

            app.Tap("LoginButton");

            var loginResult = app.WaitForElement("WrongLabel");

            //Assert
            Assert.That(loginResult, Is.Not.Null);
        }

        [Test]
        public void LoginButton_EnterUsernametestAndPWWrongPressLoginButton_WrongLabelShows()
        {

            //Arrange
            app.Tap("UsernameEntry");

            //Act
            app.EnterText("UsernameEntry", "test");
            app.PressEnter();
            app.Tap("PasswordEntry");
            app.EnterText("PasswordEntry", "Wrong");
            app.PressEnter();

            app.Tap("LoginButton");
            //TODO Appen crasher når der hentes forkert data fra DB

            var loginResult = app.WaitForElement("WrongLabel");

            //Assert
            Assert.That(loginResult, Is.Not.Null);
        }

        


    }
}