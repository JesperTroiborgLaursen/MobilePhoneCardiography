using NUnit.Framework;

namespace PresentationLayer.Test.Unit
{
    public class MeasureViewModel_Test_Unit
    {
        

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void HandleAnalyzeFinished_CallEvent_EventRaisesNewEvent()
        {
            //ARRANGE


            //ACT
            fakeRfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFID = rfid });
            
            //ASSERT
            Assert.Pass();


        }
    }
}