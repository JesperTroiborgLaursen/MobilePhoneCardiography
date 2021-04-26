using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.Interface
{
    public interface IRecorder
    {
        void RecordAudio();
        IAudioRecorderService RecorderService { get; set; }
        Task ConcurrentStream();
     
    }


}
