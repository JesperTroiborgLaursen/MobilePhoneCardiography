using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IRecorderController
    {
        void PlayRecording();
        Task RecordAudio();


    }
}
