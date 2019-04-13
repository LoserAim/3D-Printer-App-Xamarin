using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Models
{

    /*
     Default Statuses:
     Approved
     Denied
     nostatus
     Busy
     Open
     Closed
         
         */
    public class Status
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique,NotNull,MaxLength(10)]
        public string Name { get; set; }
        public static async Task<int> Insert(Status status)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(status);
            
            return rows;
        }
        public static async Task<List<Status>> GetAll()
        {
            List<Status> statuses = new List<Status>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            await conn.CreateTableAsync<Status>();

            statuses = await conn.Table<Status>().ToListAsync();

            
            return statuses;
        }
        public static async Task<Status> SearchByName(string searchText = null)
        {
            List<Status> statuses = await GetAll();

            return statuses.FirstOrDefault(g => g.Name == searchText);
        }
        public static async Task<Status> SearchByID(int ID)
        {
            List<Status> statuses = await GetAll();

            return statuses.FirstOrDefault(g => g.ID == ID);
        }

    }
}
