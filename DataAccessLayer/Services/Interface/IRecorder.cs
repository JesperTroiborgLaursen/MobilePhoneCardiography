using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EventArgss;

namespace DataAccessLayer.Services.Interface
{
    public interface IRecorder
    {
        void RecordAudio();
        Stream SequenceStream { get; set; }
    }
}
