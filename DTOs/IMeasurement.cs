using System;
using System.IO;

namespace DTOs
{
    public interface IMeasurement
    {
        public int Id { get; set; }
        public Stream HeartSound { get; set; }
        public DateTime StartTime { get; set; }
        public int ProbabilityProcent { get; set; }
        public string PatientID { get; set; }
        public string HealthProfID { get; set; }

        public PlacementOfDeviceEnum PlacementEnum { get; set; }

    }
}