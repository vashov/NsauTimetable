using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NsauT.Client.DAL.DataStore
{
    public interface IRepository<T> : IDisposable
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
