using System;
using DTOs;


namespace EventArgss
{
    public class RecordFinishedEventArgs: EventArgs
    {
        public Measurement measureDTO { get; set; }
    }
}