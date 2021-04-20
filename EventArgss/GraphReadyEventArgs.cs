using System;
using System.Collections.Concurrent;
using Microcharts;

namespace EventArgss
{
    public class GraphReadyEventArgs : EventArgs
    {
        
        public ChartEntry[] ChartValues { get; set; }
    }
}