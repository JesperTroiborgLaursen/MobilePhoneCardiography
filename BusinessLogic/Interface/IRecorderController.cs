using System;
using System.IO;
using System.Threading.Tasks;
using DTOs;

namespace BusinessLogic
{
    public interface IRecorderController
    {
        void PlayRecording(Measurement measurement);
        void RecordAudio();
        bool IsRecording { get; }
        string PageText { get; set; }

    }
}
