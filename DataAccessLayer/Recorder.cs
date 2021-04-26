using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Services.Interface;
using DTOs;
using EventArgss;
using Plugin.AudioRecorder;
using Xamarin.Cognitive.Speech;

namespace DataAccessLayer
{
    public class Recorder : IRecorder
    {
        #region Dependencies
        public IAudioRecorderService RecorderService { get; set; }

        public Stream SequenceStream { get; set; }

        public ITimeProvider _timeProvider;
        #endregion
        #region Event

        public event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
        protected virtual void OnRecordingFinished(RecordFinishedEventArgs e)
        {
            RecordFinishedEvent?.Invoke(this, e);
        }

        #endregion
        #region Ctor

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler, IAudioRecorderService audioRecorderService,
            ITimeProvider timeProvider)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            RecorderService = audioRecorderService ?? new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _timeProvider = timeProvider ?? new RealTimeProvider();
        }

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            RecorderService = new ExtendedAudioRecorderService(HandleRecorderIsFinished);

            _timeProvider = new RealTimeProvider();


        }
        #endregion
        #region Metoder

        public void RecordAudio()
        {
            _timeProvider.StartTimer();
            RecorderService.StartRecording();
        }

        public async Task ConcurrentStream()
        {
            var audioRecordTask = await RecorderService.StartRecording();

            if (RecorderService.IsRecording)
            {
                using (var stream = RecorderService.GetAudioFileStream())
                {
                    SequenceStream = stream;
                    AudioFunctions.WriteWavHeader(SequenceStream, 2, 44100, 8);

                    //private Stream LocateTrainingSound()
                    //{

                    //    Task<Stream> read = FileSystem.OpenAppPackageFileAsync("HeartSounds\\Abno_403_10TIL_20_e01732.wav");
                    //    while (read.IsCompleted != true) { }
                    //    return read.Result;
                    //}

                    //    //Todo check values in the WriteWavHeader
                    //    stream.CopyTo(SequenceStream);
                    //    System.Diagnostics.Debug.WriteLine("Test");

                    //    Task<Stream> readAudio = RecorderLogic.GetAudioFileStream();
                    //    while (readAudio.IsCompleted != true) { } ;
                    //    return readAudio.Result;
                    //}
                }   
                
            }


        }

        #endregion
        #region EventHandler

        public void HandleRecorderIsFinished(object sender, string e)
        {
            _timeProvider.StopTimer(true, true);

            Measurement tempMeasureDTO = new Measurement(_timeProvider.GetDateTime());

            var stream = RecorderService.GetAudioFileStream();

            tempMeasureDTO.HeartSound = stream;


            OnRecordingFinished(new RecordFinishedEventArgs
            {
                measureDTO = tempMeasureDTO
            });
        }

        #endregion
    }
}
