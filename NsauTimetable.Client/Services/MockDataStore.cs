using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NsauTimetable.Client.Models;

namespace NsauTimetable.Client.Services
{
    public class MockDataStore : IDataStore<TimetableItem>
    {
        readonly List<TimetableItem> items;

        public MockDataStore()
        {
            items = new List<TimetableItem>()
            {
                new TimetableItem { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new TimetableItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new TimetableItem { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new TimetableItem { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new TimetableItem { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new TimetableItem { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(TimetableItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TimetableItem item)
        {
            var oldItem = items.Where((TimetableItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((TimetableItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TimetableItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TimetableItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}