using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using BusinessLogic;
using DataAccessLayer.Services.Interface;
using DTOs;
using EventArgss;
using Microcharts;
using NSubstitute;

namespace BuisnessLogic.Test.Unit
{
    class RecorderController_Test_Unit
    {
        private RecorderController UUT;
        private IRecorder sub_Recorder;

        private ISoundModifyLogic sub_soundModifyLogic;
        private IAnalyzeLogic sub_analyse;
        private ISaveData sub_dataStorage;
        private EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent;
        private RecordFinishedEventArgs finishedEvent;

        [SetUp]
        public void Setup()
        {
            sub_Recorder = Substitute.For<IRecorder>();
            sub_soundModifyLogic = Substitute.For<ISoundModifyLogic>();
            sub_analyse = Substitute.For<IAnalyzeLogic>();
            sub_dataStorage = Substitute.For<ISaveData>();
            UUT = new RecorderController(handleAnalyzeFinishedEvent, sub_Recorder, sub_soundModifyLogic, sub_analyse,
                sub_dataStorage);
        }

        [Test]
        public void RecordAudio_OneCall_StartIsRecordingEqFalse_ReceivedCallIsOne()
        {
            UUT.RecordAudio();

            Assert.Multiple(
                () =>
                {
                    sub_Recorder.Received(1).RecordAudio();
                    Assert.That(UUT.IsRecording);
                });
        }

        [Test]
        public void RecordAudio_TwoCall_ReceivedCallIsOne()
        {
            UUT.RecordAudio();
            UUT.RecordAudio();

            Assert.Multiple(
                () =>
                {
                    sub_Recorder.Received(1).RecordAudio();
                    Assert.That(UUT.IsRecording);
                });
        }

        [Test]
        public void PlayRecording_OneCall_soundModifyLogic_ReceivedOneCall()
        {
            //ARRANGE
            Measurement dto = new Measurement(new DateTime(1, 1, 1));
            UUT.PlayRecording(dto);

            sub_soundModifyLogic.Received(1).PlayRecording(dto.HeartSound);
        }

        [Test]
        public void ProcessStreamValuesReadToEnd_ReturnsChartEntry()
        {
            //ARRANGE
            var test_Stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));

            //ACT
            var result = UUT.ProcessStreamValues(test_Stream);

            // Assert    
            Assert.That(result, Is.TypeOf<ChartEntry[]>());
        }
        
    }
}
