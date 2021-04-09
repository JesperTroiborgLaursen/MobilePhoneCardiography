using NUnit.Framework;
using Xamarin.UITest;

namespace UITest2
{

    [TestFixture(Platform.Android)]
    public class UTMeasureView
    {
        IApp app;
        Platform platform;

        public UTMeasureView(Platform platform)
        {
            this.platform = platform;

        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            app.Tap("PrivateUserLoginButton");
            app.Tap(c => c.Marked("Measure"));
        }



    }
}