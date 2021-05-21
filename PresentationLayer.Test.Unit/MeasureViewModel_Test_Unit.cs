using NUnit.Framework;
using MobilePhoneCardiography.ViewModels;
using BusinessLogic;
using NSubstitute;
using DataAccessLayer.Services.Interface;
using DataAccessLayer;
using EventArgss;
using System;


namespace PresentationLayer.Test.Unit
{
    public class MeasureViewModel_Test_Unit
    {
        private MeasureViewModel uut;
        private IRecorderController fakeRecorderController;
        private IRecorder fakeRecorder;
        private ExtendedAudioRecorderService fakeAudioRecorderService;

        [SetUp]
        public void Setup()
        {
            uut = new MeasureViewModel();
            fakeRecorder = Substitute.For<IRecorder>();
            fakeRecorderController = Substitute.For<IRecorderController>();
            fakeAudioRecorderService = Substitute.For<ExtendedAudioRecorderService>();
        }

        [Test]
        public void HandleAnalyzeFinished_CallEvent_EventRaisesNewEvent()
        {
            //ARRANGE
            

            //ACT
            //fakeRecorder.
            //fakeRfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFID = rfid });
            
            //ASSERT
            Assert.Pass();


        }
    }
}