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
        private bool _isRecording; //Todo Få knapper på UI til at være inaktive når der optages (Fx.)

        public bool IsRecording
        {
            get { return _isRecording; }
            private set { _isRecording = value; }
        }

        private Measurement _measureDTO;

        public Measurement MeasureDTO
        {
            get { return _measureDTO; }
            set { _measureDTO = value; }
        }
        public ChartEntry[] ChartValues { get; set; }

        #endregion 
        #region Dependencies

        private IRecorder _recorderLogic;
        private ISoundModifyLogic _soundModifyLogic;
        private IAnalyzeLogic _analyse;
        private ISaveData _dataStorage;
        #endregion
        #region Ctor

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            _recorderLogic = new Recorder(HandleRecordingFinishedEvent);
            _soundModifyLogic = new SoundModifyLogic(null);
            _analyse = new AnalyzeLogic(handleAnalyzeFinishedEvent);
            _dataStorage = new FakeStorage(); //ligger som internal class

            IsRecording = false;

        }

        public RecorderController(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent, IRecorder recorder, ISoundModifyLogic soundModifyLogic, IAnalyzeLogic analyzeLogic, ISaveData saveData)
        {
            _recorderLogic = recorder ?? new Recorder(HandleRecordingFinishedEvent);
            _soundModifyLogic = soundModifyLogic ?? new SoundModifyLogic(null);
            _analyse = analyzeLogic ?? new AnalyzeLogic(handleAnalyzeFinishedEvent);
            _dataStorage = saveData ?? new FakeStorage();

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

            byte[] tempBytes = ReadToEnd(recording);  //TODO how do we fix this method ReadFully(recording);
            ChartEntry[] entries = new ChartEntry[tempBytes.Length];

            int i = 0;
            foreach (var tempByte in tempBytes)
            {
                entries[i] = new Microcharts.ChartEntry(tempByte);
                //{

                //    Label = "sample",
                //    ValueLabel = tempByte.ToString(),
                //    Color = SKColor.Parse("#b455b6")


                //};
                i++;
            }
            return entries;

        }

        //TODO Temporary method to make the convert from stream to byte work
        private static byte[] ReadToEnd(System.IO.Stream stream)
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


        //TODO Temporary method - delete when Mads and Emils branch is merged to master
        //public byte[] ReadFully(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];

        //    int read;
        //    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //    {
        //        ms.Write(buffer, 0, read);
        //    }
        //    return ms.ToArray();


        //}
        #endregion
        #region EventHandler

        private void HandleRecordingFinishedEvent(object sender, RecordFinishedEventArgs e)
        {
            IsRecording = false;
            MeasureDTO = e.measureDTO;
            MeasureDTO = _analyse.Analyze(MeasureDTO);
            _dataStorage.SaveToStorage(MeasureDTO);
            ChartValues = ProcessStreamValues(MeasureDTO.HeartSound);
            

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
