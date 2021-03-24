using System;
using DTOs;
using EventArgss;

namespace BusinessLogic
{
    public interface IAnalyzeLogic
    {
        event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;
        Measurement Analyze(Measurement DTO);
    }
}