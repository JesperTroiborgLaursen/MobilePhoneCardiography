using NUnit.Framework;
using Xamarin.UITest;

namespace UITest2
{

    [TestFixture(Platform.Android)]
    public class UTMeasurementDetailView
    {
        IApp app;
        Platform platform;

        public UTMeasurementDetailView(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }
    }
}