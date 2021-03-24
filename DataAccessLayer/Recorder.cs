﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Services.Interface;
using DTOs;
using EventArgss;

namespace DataAccessLayer
{
    public class Recorder : IRecorder
    {
        #region Dependencies
        public IAudioRecorderService _recorder;
        public ITimeProvider _timeProvider;
        #endregion
        #region Event

        public event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
        protected virtual void OnRecordingFinished(RecordFinishedEventArgs e)
        {
            RecordFinishedEvent?.Invoke(this, e);
        }

        #endregion

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler, IAudioRecorderService audioRecorderService,
            ITimeProvider timeProvider)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            _recorder = audioRecorderService ?? new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _timeProvider = timeProvider ?? new RealTimeProvider();
        }

        public Recorder(EventHandler<RecordFinishedEventArgs> recordFinishedEventHandler)
        {
            RecordFinishedEvent += recordFinishedEventHandler;

            _recorder = new ExtendedAudioRecorderService(HandleRecorderIsFinished);
            _timeProvider = new RealTimeProvider();
        }

        public void RecordAudio()
        {
            _timeProvider.StartTimer();
            _recorder.StartRecording();
        }

        public void HandleRecorderIsFinished(object sender, string e)
        {
            _timeProvider.StopTimer(true, true);

            Measurement tempMeasureDTO = new Measurement(_timeProvider.GetDateTime());

            var stream = _recorder.GetAudioFileStream();

            tempMeasureDTO.HeartSound = stream;


            OnRecordingFinished(new RecordFinishedEventArgs
            {
                measureDTO = tempMeasureDTO
            });
        }
    }
}
