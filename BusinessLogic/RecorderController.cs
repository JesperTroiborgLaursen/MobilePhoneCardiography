using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOs;
using EventArgss;


namespace BusinessLogic
{
    public class RecorderController : IRecorderController
    {

        private Measurement measureDTO;

        private string _pageText = "Du har nu åbnet Plugin Audio Recorder skærmen";

        public string PageText
        {
            get { return _pageText; }
            set { _pageText = value; }
        }
        //RecorderController skal sørge for at udskrive vigtige meddelelser ifbm. recording!!!!!!!!!!!
        private string _sampleRate;

        public string SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }


        private IRecorderLogic _recorderLogic;
        private ISoundModifyLogic _soundModifyLogic;
        private IAnalyzeLogic _analyse;
        private ISaveData _dataStoreage;

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            _recorderLogic = new FakeRecorderLogic(HandleRecordingFinishedEvent);
            _soundModifyLogic = new FakeSoundModifyLogic();

            _analyse = new AnalyzeLogic();
            _analyse.AnalyzeFinishedEvent += handleAnalyzeFinishedEvent;


            _dataStoreage = new FakeStorage();
        }


        public void PlayRecording()
        {
            _soundModifyLogic.PlayRecording(MeasureDTO.HeartSound);
        }

        public async Task RecordAudio()
        {
            await _recorderLogic.RecordAudio();
        }
        private Measurement _measureDTO;

        public Measurement MeasureDTO
        {
            get { return _measureDTO; }
            set { _measureDTO = value; }
        }

        private void HandleRecordingFinishedEvent(object sender, RecordFinishedEventArgs e)
        {
            
            MeasureDTO = e.measureDTO;
            MeasureDTO = _analyse.Analyze(MeasureDTO);
            _dataStoreage.SaveToStorage(_measureDTO);
        }
    }

    /// <summary>
    /// Fake recorderLogic, der stubber resten af systemet ned til.
    /// Denne Rasiser blot et event, for at ilustere hvad processen er der fra
    /// </summary>
    public class FakeRecorderLogic : IRecorderLogic
    {
        private readonly EventHandler<RecordFinishedEventArgs> _handleRecordingFinishedEvent;

        public FakeRecorderLogic(EventHandler<RecordFinishedEventArgs> handleRecordingFinishedEvent)
        {
            _handleRecordingFinishedEvent = handleRecordingFinishedEvent;
        }

        public async Task RecordAudio()
        {
            System.Diagnostics.Debug.WriteLine("Der er trykket start");
            _handleRecordingFinishedEvent?.Invoke(this, new RecordFinishedEventArgs
            {
                measureDTO = new Measurement(DateTime.Now)
            });

        }
    }

    /// <summary>
    /// Fake lydafspiller
    /// </summary>
    public class FakeSoundModifyLogic : ISoundModifyLogic
    {
        public void PlayRecording()
        {
           // throw new NotImplementedException();
        }

        public void PlayRecording(Stream sound)
        {
           // throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Fake storage funktion
    /// </summary>
    internal class FakeStorage : ISaveData
    {
        public void SaveToStorage(Measurement elementToStorage)
        {
            System.Diagnostics.Debug.WriteLine("Dine data er nu gemt");
        }
    }
}
