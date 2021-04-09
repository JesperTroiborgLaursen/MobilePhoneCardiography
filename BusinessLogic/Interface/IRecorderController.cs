﻿using System;
using System.IO;
using System.Threading.Tasks;
using DTOs;
using Microcharts;

namespace BusinessLogic
{
    public interface IRecorderController
    {
        void PlayRecording(Measurement measurement);
        void RecordAudio();
        public ChartEntry[] PlotRecording(Stream recording);
        bool IsRecording { get; }
        string PageText { get; set; }

    }
}
