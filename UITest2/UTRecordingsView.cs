using NUnit.Framework;
using Xamarin.UITest;

namespace UITest2
{
    [TestFixture(Platform.Android)]
    public class UTRecordingsView
    {
        IApp app;
        Platform platform;

        public UTRecordingsView(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            app.Tap("PrivateUserLoginButton");
        }
    }
}