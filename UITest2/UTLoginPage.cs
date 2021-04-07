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

            //Act
            app.Tap("PrivateUserLoginButton");
            var tabElement = app.WaitForElement("RecordingsTab");

            //Assert
            Assert.That(tabElement, Is.Not.Null);
        }


        [Test]
        public void HealthUserLoginButton_PressHealthUserLoginButton_HealthLoginTabOpened()
        {
            //Arrange

            //Act
            app.Tap("HealthUserLoginButton");
            var tabElement = app.WaitForElement("HealthUserLoginTab");

            //Assert
            Assert.That(tabElement, Is.Not.Null);
        }



    }
}