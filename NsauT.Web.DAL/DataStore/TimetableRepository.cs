using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NsauT.Web.DAL.DataStore
{
    public class TimetableRepository : IRepository<TimetableEntity>
    {
        private TimetableAppContext _context;

        public TimetableRepository(string connectionString)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
            _context = new TimetableAppContext(options);
        }

        public async Task<bool> AddItemAsync(TimetableEntity item)
        {
            await _context.Timetables.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            TimetableEntity entity = await GetItemAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Timetables.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TimetableEntity> GetItemAsync(string id)
        {
            TimetableEntity timetable = await _context.Timetables
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            return timetable;
        }

        public async Task<IEnumerable<TimetableEntity>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _context.Timetables
                .AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(TimetableEntity item)
        {
            _context.Timetables.Update(item);
            await _context.SaveChangesAsync();

            //MessagingCenter.Send<IRepository<TimetableEntity>>(this, MessageKeys.UpdateBillItem);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
