using System;
using System.IO;

namespace EventArgss
{
    public class StreamSequenceOngoingArgs: EventArgs
    {
        public Stream Sequence { get; set; }
    }
}