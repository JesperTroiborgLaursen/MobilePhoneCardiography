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
        public AnalyzeLogic(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            AnalyzeFinishedEvent += handleAnalyzeFinishedEvent;
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
            analysisObject.ProbabilityProcent = Analyze1();
            OnAnalyzeFinished(new AnalyzeFinishedEventArgs()
            {
                DTO = analysisObject
            });
            return analysisObject;
        }

        //ToDo Vi skal have rette dette så vi ikke er låst til "13%"
        //Her skal vi analysere dataen
        private int Analyze1()
        {
            return 13;

        }

        private Stream GetTestSound()
        { 
            Task<Stream> read = FileSystem.OpenAppPackageFileAsync("TestHjertelyd1_44100.wav");
            while(read.IsCompleted!=true){}
            return read.Result;
        }
    }
}