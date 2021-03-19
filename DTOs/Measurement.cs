using System;
using System.IO;

namespace DTOs
{
    public enum PlacementOfDeviceEnum
    {
        CorDexter = 1,
        CorSinister = 2,
        CorInfra = 3
    };


    public class Measurement
    {
        public Stream SoundStream { get; set; }
        public DateTime StartTime { get; set; }
        public int ProbabilityProcent { get; set; }
        public int PatientID { get; set; }
        public int HealthProfessionalID { get; set; }

        public PlacementOfDeviceEnum PlacementEnum { get; set; }
        public Measurement(DateTime start)
        {
            StartTime = start;
        }
    }
}
