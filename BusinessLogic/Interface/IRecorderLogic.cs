using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IRecorderLogic
    {
        Task RecordAudio();
    }
}