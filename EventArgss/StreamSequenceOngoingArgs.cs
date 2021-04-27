using System;
using System.IO;

namespace EventArgss
{
    //TODO delete this event?
    public class StreamSequenceOngoingArgs: EventArgs
    {
        public Stream Sequence { get; set; }
    }
}