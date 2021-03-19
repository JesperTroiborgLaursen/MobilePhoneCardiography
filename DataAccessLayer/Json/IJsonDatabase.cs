using System;

namespace MobilePhoneCardiography.Models.Json
{
    public interface IJsonDatabase
    {
        public string id { get; set; }
        public string PatientID { get; set; }
        public DateTime date { get; set; }
    }
}