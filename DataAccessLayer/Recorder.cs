using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.Services.Interface;
using DTOs;
using EventArgss;
using Plugin.AudioRecorder;

namespace DataAccessLayer
{
    public class Recorder : IRecorder
    {
        #region Dependencies
        public IAudioRecorderService RecorderService { get; set; }
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
            //Todo indkommenter igen, og fjern concurrent stream hvis det fejler!!
            RecorderService.StartRecording(); 
            //ConcurrentStream(); // samtidig stream af lyd til graf, uden lydfilen er færdig!
        }

        public Stream SequenceStream { get; set; }

        //TODO delete or change this method
        public async void ConcurrentStream()
        {
            var audioRecordTask = await RecorderService.StartRecording();
            
            if (RecorderService.IsRecording)
            {
                using (var stream = RecorderService.GetAudioFileStream())
                {
                    //Todo check values in the WriteWavHeader
                    AudioFunctions.WriteWavHeader(stream, 2, 44100, 8);
                    SequenceStream = stream;
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
