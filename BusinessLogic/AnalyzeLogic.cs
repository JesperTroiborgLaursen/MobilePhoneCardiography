using System;
using System.Diagnostics;
using DTOs;
using EventArgss;

namespace BusinessLogic
{
    public class AnalyzeLogic : IAnalyzeLogic
    {
        private Measurement analysisObject;

        public Measurement Analyze(Measurement DTO)
        {
            analysisObject = DTO;
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
            Debug.WriteLine("Data bliver nu analyseret");
            return 13;
        }

        protected virtual void OnAnalyzeFinished(AnalyzeFinishedEventArgs e)
        {
            AnalyzeFinishedEvent?.Invoke(this, e);
        }

        public event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;

    }
}