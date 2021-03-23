using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IRecorderController
    {
        void PlayRecording();
        void RecordAudio();
        bool IsRecording { get; }
        string PageText { get; set; }

    }
}
