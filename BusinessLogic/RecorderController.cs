using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Services.Interface;
using DTOs;
using EventArgss;
using Microcharts;
using SkiaSharp;

//temp added
using MobilePhoneCardiography.Services.DataStore;

namespace BusinessLogic
{

    public class RecorderController : IRecorderController
    {
        #region Props
        //RecorderControllerens opgave er at udskrive vigtige meddelelser ifbm. recording!!!!!!!!!!!
  
        private bool _isRecording; //Todo Få knapper på UI til at være inaktive når der optages (Fx.)
        private Measurement _measureDTO;

        public ChartEntry[] ChartValues { get; set; }
        public bool IsRecording
        {
            get { return _isRecording; }
            private set { _isRecording = value; }
        }
        #endregion 
        #region Dependencies

        private IRecorder _recorderLogic;
        private ISoundModifyLogic _soundModifyLogic;
        private IAnalyzeLogic _analyse;
        private ISaveData _dataStorage;
        private IGraphFeatures _graphFeatures;
        #endregion
        #region Ctor

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            _recorderLogic = new Recorder(HandleRecordingFinishedEvent);
            _soundModifyLogic = new SoundModifyLogic(null);
            _analyse = new AnalyzeLogic(handleAnalyzeFinishedEvent);
            _dataStorage = new FakeStorage(); //ligger som internal class
            _graphFeatures = new GraphFeatures();
            IsRecording = false;
        }

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent, IRecorder recorder, ISoundModifyLogic soundModifyLogic, IAnalyzeLogic analyzeLogic, ISaveData saveData)
        {
            _recorderLogic = recorder ?? new Recorder(HandleRecordingFinishedEvent);
            _soundModifyLogic = soundModifyLogic ?? new SoundModifyLogic(null);
            _analyse = analyzeLogic ?? new AnalyzeLogic(handleAnalyzeFinishedEvent);
            _dataStorage = saveData ?? new FakeStorage();
            _graphFeatures = new GraphFeatures();
        }

        #endregion
        #region Metoder


        public void PlayRecording(Measurement measurement)
        {
            _soundModifyLogic.PlayRecording(measurement.HeartSound);
        }


        public void RecordAudio()
        {
            if (IsRecording == false)
            {
                _recorderLogic.RecordAudio();
                IsRecording = true;
            }
        }

        public ChartEntry[] ProcessStreamValues(Stream recording)
        {
            byte maxByteValue = 127;
            byte dB;
            byte[] everyRecordingByte = ReadToEnd(recording);  //TODO how do we fix this method ReadFully(recording);
            byte[] downSampledRecording = _graphFeatures.DownSample(everyRecordingByte);
            ChartEntry[] entries = new ChartEntry[downSampledRecording.Length];

            int i = 0;
            //entries[i] = new Microcharts.ChartEntry(300) { ValueLabel = "indikator", Color = SKColor.FromHsl(2,2,100,100)};
            //i++;
            foreach (var bytes in downSampledRecording)
            {
                if (bytes != 0)
                {
                    dB = (byte)(20 * Math.Log10(Math.Abs(bytes))); //Converts from byte in wav to dB. Formula: 20 * logbase10 (amplitude1 / amplitude2)//20*log10((x-128)/255) gives dB FS.
                }
                else { dB = bytes; }
                entries[i] = new Microcharts.ChartEntry(bytes);
                i++;
            }
            return entries;

        }
        
        private byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        #endregion
        #region EventHandler

        private void HandleRecordingFinishedEvent(object sender, RecordFinishedEventArgs e)
        {
            IsRecording = false;
            _measureDTO = e.measureDTO;
            ChartValues = ProcessStreamValues(_measureDTO.HeartSound);
            _measureDTO = _analyse.Analyze(_measureDTO);
            _dataStorage.SaveToStorage(_measureDTO);
        }

        #endregion
    }

    internal class FakeStorage : ISaveData
    {
        public void SaveToStorage(Measurement elementToStorage)
        {
            System.Diagnostics.Debug.WriteLine("Dine data er nu gemt");
        }
    }
}
