using System;
using DTOs;

namespace EventArgss
{
    public class AnalyzeFinishedEventArgs: EventArgs
    {
        public Measurement DTO { get; set; }
    }
}