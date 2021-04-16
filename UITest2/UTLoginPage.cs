using NUnit.Framework;
using Xamarin.UITest;

namespace UITest2
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class UTLoginPage
    {

        IApp app;
        Platform platform;

        public UTLoginPage(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }


        [Test]
        public void PrivateUserLoginButton_PressPrivateUserLoginButton_RecordingsTabOpened()
        {
            //Arrange
            //TODO: Need to retrieve patient or have mock data to test this

            //Act
            app.Tap("PrivateUserLoginButton");
            var tabElement = app.WaitForElement("RecordingsList");

            //Assert
            Assert.That(tabElement, Is.Not.Null);
        }


        [Test]
        public virtual void HealthUserLoginButton_PressHealthUserLoginButton_HealthLoginTabOpened()
        {
            //Arrange

            //Act
            app.Tap("HealthUserLoginButton");
            //If UsernameEntry is appearing the tab has opened -> this is a workaround, because automationIds dont work for tabs.
            var tabElement = app.WaitForElement("UsernameEntry");

            //Assert
            Assert.That(tabElement, Is.Not.Null);
        }



    }
}