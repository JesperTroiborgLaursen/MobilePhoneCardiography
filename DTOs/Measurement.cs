﻿using System;
using System.IO;

namespace DTOs
{
    public enum PlacementOfDeviceEnum
    {
        URSB = 1,
        ULSB = 2,
        LLSB= 3,
        Apex = 4

    };


    public class Measurement
    {
        public int Id { get; set; }
        public Stream HeartSound { get; set; }
        public DateTime StartTime { get; set; }
        public int ProbabilityProcent { get; set; }
        public string PatientID { get; set; }
        public string HealthProfID { get; set; }

        public PlacementOfDeviceEnum PlacementEnum { get; set; }

        public Measurement()
        {
            
        }
        public Measurement(DateTime startTime)
        {
            StartTime = startTime;
        }
      
    }
}
