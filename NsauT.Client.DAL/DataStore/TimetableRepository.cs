using NsauT.Client.DAL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NsauT.Client.DAL.DataStore
{
    public class TimetableRepository : IRepository<TimetableEntity>
    {
        private readonly string _dbPath;
        private SQLiteAsyncConnection _connection;
        private bool _createTableExcecuted = false;

        private SQLiteAsyncConnection Connection 
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteAsyncConnection(_dbPath);
                }
                return _connection;
            } 
        }

        public TimetableRepository(string connectionString)
        {
            _dbPath = connectionString;
        }

        public async Task CreateTable()
        {
            await Connection.CreateTableAsync<TimetableEntity>();
            _createTableExcecuted = true;
        }

        private async Task EnsureCreateTableExcecuted()
        {
            if (_createTableExcecuted)
                return;
            await CreateTable();
        }

        public async Task<bool> AddItemAsync(TimetableEntity item)
        {
            await EnsureCreateTableExcecuted();

            int insertedCount = await Connection.InsertAsync(item, typeof(TimetableEntity));
            if (insertedCount == 1)
            {
                //MessagingCenter.Send<IRepository<TimetableEntity>>(this, MessageKeys.AddBillItem);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            await EnsureCreateTableExcecuted();

            TimetableEntity timetableForDeleting = await Connection.Table<TimetableEntity>()
                .FirstOrDefaultAsync(t => t.Id == id);
            if (timetableForDeleting == null)
            {
                return false;
            }

            await Connection.DeleteAsync<TimetableEntity>(id);
            //MessagingCenter.Send<IRepository<TimetableEntity>>(this, MessageKeys.DeleteBillItem);
            return true;
        }

        public async Task<TimetableEntity> GetItemAsync(int id)
        {
            await EnsureCreateTableExcecuted();

            return await Connection.Table<TimetableEntity>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TimetableEntity>> GetItemsAsync(bool forceRefresh = false)
        {
            await EnsureCreateTableExcecuted();

            return await Connection.Table<TimetableEntity>().ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(TimetableEntity item)
        {
            await EnsureCreateTableExcecuted();

            TimetableEntity timetableForUpdating = await Connection.Table<TimetableEntity>()
                .FirstOrDefaultAsync(bill => bill.Id == item.Id);
            if (timetableForUpdating == null)
            {
                return false;
            }

            await Connection.UpdateAsync(item, typeof(TimetableEntity));
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
                if (_connection != null)
                {
                    var conn = _connection;
                    _connection = null;
                    conn.GetConnection().Close();
                    conn.GetConnection().Dispose();
                    SQLiteAsyncConnection.ResetPool();
                }
            }
        }
    }
}
