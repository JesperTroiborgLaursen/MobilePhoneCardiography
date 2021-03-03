using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobilePhoneCardiography.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T measurement);
        Task<bool> UpdateItemAsync(T user);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
