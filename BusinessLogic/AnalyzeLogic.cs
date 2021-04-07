using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DTOs;
using EventArgss;
using Xamarin.Essentials;

namespace BusinessLogic
{


    public class AnalyzeLogic : IAnalyzeLogic
    {
        private Measurement analysisObject;
        private IFFT interfaceFFT;

        public AnalyzeLogic(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent, IFFT interfaceFFT)
        {
            AnalyzeFinishedEvent += handleAnalyzeFinishedEvent;
            this.interfaceFFT = interfaceFFT;

        }
        #region Event
        public event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;
        protected virtual void OnAnalyzeFinished(AnalyzeFinishedEventArgs e)
        {
            AnalyzeFinishedEvent?.Invoke(this, e);
        }
        #endregion

        public Measurement Analyze(Measurement DTO)
        {
            analysisObject = DTO;
            analysisObject.HeartSound = GetTestSound();
            analysisObject = interfaceFFT.Analyze(analysisObject);
            OnAnalyzeFinished(new AnalyzeFinishedEventArgs()
            {
                DTO = analysisObject
            });
            return analysisObject;
        }

        private Stream GetTestSound()
        { 
            Task<Stream> read = FileSystem.OpenAppPackageFileAsync("TestHjertelyd1_44100.wav");
            while(read.IsCompleted!=true){}
            return read.Result;
        }
    }
}