using System;
using Microcharts;

namespace EventArgss
{
    public class GraphReadyEventArgs : EventArgs
    {
        public ChartEntry[] ChartValues { get; set; }
    }
}