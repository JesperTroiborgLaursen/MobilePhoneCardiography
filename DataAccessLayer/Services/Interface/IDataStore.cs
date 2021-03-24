using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T measurement);
        Task<bool> UpdateItemAsync(T user);
        Task<bool> DeleteItemAsync(long id);
        Task<T> GetItemAsync(long id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
