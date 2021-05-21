using System;
using System.IO;
using System.Threading.Tasks;
using DTOs;
using EventArgss;
using Microcharts;

namespace BusinessLogic
{
    public interface IRecorderController
    {
        void PlayRecording(Measurement measurement);
        void RecordAudio();
        public ChartEntry[] ProcessStreamValues(Stream recording);
        public ChartEntry[] ChartValues { get; set; }
        bool IsRecording { get; }
        public void HandleRecordingFinishedEvent(object sender, RecordFinishedEventArgs e);



    }
}
