using System;
using System.Collections.Generic;
using System.Text;
using EventArgss;

namespace DataAccessLayer.Services.Interface
{
    public interface IRecorder
    {
        void RecordAudio();
        public event EventHandler<RecordFinishedEventArgs> RecordFinishedEvent;
    }
}
