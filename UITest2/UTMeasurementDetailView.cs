//using System;
//using System.Linq;
//using NUnit.Framework;
//using Xamarin.UITest;

//namespace UITest2
//{

//    [TestFixture(Platform.Android)]
//    public class UTMeasurementDetailView
//    {
//        IApp app;
//        Platform platform;

//        public UTMeasurementDetailView(Platform platform)
//        {
//            this.platform = platform;
//        }

//        [SetUp]
//        public void BeforeEachTest()
//        {
//            app = AppInitializer.StartApp(platform);
//            app.Tap("PrivateUserLoginButton");
//        }

//        [Test]
//        public void Labels_LoadMockId1_Loads()
//        {

//            //Arrange 

//            //Act
//            app.Tap("4/9/2021 11:14:44 AM");
//            var date = app.Query("StartTimeLabel").FirstOrDefault().Text;
//            var risk = app.Query("ProbLabel").FirstOrDefault().Text;
//            var placement = app.Query("PlacementLabel").FirstOrDefault().Text;
//            var employeeId = app.Query("EmployeeIdLabel").FirstOrDefault().Text;

//            //Assert
//            Assert.That(date, Is.EqualTo("4/9/2021 11:14:44 AM"));
//            Assert.That(risk, Is.EqualTo("50"));
//            Assert.That(placement, Is.EqualTo("CorDexter"));
//            Assert.That(employeeId, Is.EqualTo("1"));
//        }

//        [Test]
//        public void TempoSlider_LoadMockId1_TempoSliderVisible()
//        {

//            //Arrange

//            //Act
//            app.Tap("4/9/2021 11:14:44 AM");

//            //Assert
//            try
//            {
//                app.WaitForNoElement("TempoSlider");
//            }
//            catch (Exception e)
//            {
//                Assert.Fail();
//                throw;
//            }
//            Assert.Pass();
//        }


//        [Test]
//        public void PlayButton_LoadMockId1_PlayButtonVisible()
//        {

//            //Arrange

//            //Act
//            app.Tap("4/9/2021 11:14:44 AM");

//            //Assert
//            try
//            {
//                app.WaitForNoElement("PlayButton");
//            }
//            catch (Exception e)
//            {
//                Assert.Fail();
//                throw;
//            }
//            Assert.Pass();
//        }

//        [Test]
//        public void StopButton_LoadMockId1_StopButtonVisible()
//        {

//            //Arrange

//            //Act
//            app.Tap("4/9/2021 11:14:44 AM");

//            //Assert
//            try
//            {
//                app.WaitForNoElement("StopButton");
//            }
//            catch (Exception e)
//            {
//                Assert.Fail();
//                throw;
//            }
//            Assert.Pass();
//        }

//        [Test]
//        public void RewindButton_LoadMockId1_RewindButtonVisible()
//        {

//            //Arrange

//            //Act
//            app.Tap("4/9/2021 11:14:44 AM");

//            //Assert
//            try
//            {
//                app.WaitForNoElement("RewindButton");
//            }
//            catch (Exception e)
//            {
//                Assert.Fail();
//                throw;
//            }
//            Assert.Pass();
//        }

//        [Test]
//        public void ForwardButton_LoadMockId1_ForwardButtonVisible()
//        {

//            //Arrange

//            //Act
//            app.Tap("4/9/2021 11:14:44 AM");

//            //Assert
//            try
//            {
//                app.WaitForNoElement("ForwardButton");
//            }
//            catch (Exception e)
//            {
//                Assert.Fail();
//                throw;
//            }
//            Assert.Pass();
//        }

//    }
//}