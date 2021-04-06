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





    }
}