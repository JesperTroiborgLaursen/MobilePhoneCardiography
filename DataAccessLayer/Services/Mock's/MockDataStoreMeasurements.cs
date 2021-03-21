using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DTOs;

namespace DataAccessLayer.Services
{
    public class MockDataStoreMeasurement : IDataStore<Measurement>
    {
        readonly List<Measurement> measurements;

        public MockDataStoreMeasurement()
        {
            measurements = new List<Measurement>()
            {
                new Measurement{HealthProfID = "1",PatientID = "123346-1234",PlacementEnum = PlacementOfDeviceEnum.CorDexter,
                    ProbabilityProcent = 50,HeartSound = Stream.Null, StartTime = DateTime.Now},

                new Measurement{HealthProfID = "2",PatientID = "234567-2345",PlacementEnum = PlacementOfDeviceEnum.CorInfra,
                    ProbabilityProcent = 90,HeartSound = Stream.Null, StartTime = DateTime.Now},


            };
        }
        //TODO JEG EMIL; HAR LAVET DEM LIDT OM, VI HAR LIGE NU IKKE ET MEASUREMENT ID, MEN DET GIVER NOK MENING AT HAVE NOGET MERE UNIKT AT KENDE DEM PÅ END START TID
        //TODO, OG CPR
        public async Task<bool> AddItemAsync(Measurement measurement)
        {
            measurements.Add(measurement);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Measurement measurement)
        {
            var oldMeasurement = measurements.Where((Measurement arg) => arg.StartTime == measurement.StartTime).FirstOrDefault();
            measurements.Remove(oldMeasurement);
            measurements.Add(measurement);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldMeasurement = measurements.Where((Measurement arg) => arg.StartTime ==Convert.ToDateTime(id)).FirstOrDefault();
            measurements.Remove(oldMeasurement);

            return await Task.FromResult(true);
        }

        public async Task<Measurement> GetItemAsync(long id)
        {
            return await Task.FromResult(measurements.FirstOrDefault(s => s.StartTime == Convert.ToDateTime(id)));
        }

        public async Task<IEnumerable<Measurement>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(measurements);
        }
    }
}