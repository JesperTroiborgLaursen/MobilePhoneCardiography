using System;
using System.IO;

namespace DTOs
{
    public class Patient : IPatient
    {
        public string Id { get; set; }
        public string SocSec { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}