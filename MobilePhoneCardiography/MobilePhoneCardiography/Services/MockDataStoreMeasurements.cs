using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneCardiography.Services
{
    public class MockDataStoreMeasurement : IDataStore<Measurement>
    {
        readonly List<Measurement> measurements;

        public MockDataStoreMeasurement()
        {
            measurements = new List<Measurement>()
            {
                new Measurement { Id = Guid.NewGuid().ToString(), Username = "First item", Password="This is an item description." },
                new Measurement{ Id = Guid.NewGuid().ToString(), Username = "Second item", Password="This is an item description." },
                new Measurement{ Id = Guid.NewGuid().ToString(), Username = "Third item", Password="This is an item description." },
                new Measurement{ Id = Guid.NewGuid().ToString(), Username = "Fourth item", Password="This is an item description." },
                new Measurement{ Id = Guid.NewGuid().ToString(), Username = "Fifth item", Password="This is an item description." },
                new Measurement{ Id = Guid.NewGuid().ToString(), Username = "Sixth item", Password="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Measurement measurement)
        {
            measurements.Add(measurement);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Measurement measurement)
        {
            var oldMeasurement = measurements.Where((Measurement arg) => arg.Id == measurement.Id).FirstOrDefault();
            measurements.Remove(oldMeasurement);
            measurements.Add(measurement);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldMeasurement = measurements.Where((Measurement arg) => arg.Id == id).FirstOrDefault();
            measurements.Remove(oldMeasurement);

            return await Task.FromResult(true);
        }

        public async Task<Measurement> GetItemAsync(string id)
        {
            return await Task.FromResult(measurements.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Measurement>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(measurements);
        }
    }
}