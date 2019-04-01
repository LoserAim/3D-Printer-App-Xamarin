using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace PrintQue.Models
{
    public class Request
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int PrinterID { get; set; }
        public int StatusID { get; set; }
        public int UserID { get; set; }
        public DateTime DateMade { get; set; }
        public DateTime DateRequested { get; set; }
        public int Duration { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Personal { get; set; }

        public static async Task<int> Insert(Request request)
        {
            if (request.PrinterID == 0 || request.UserID == 0)
            {
                return 0;
            }
            else
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

                var rows = await conn.InsertAsync(request);
                await conn.CloseAsync();
                return rows;
            }
        }

        //This function gets all requests in the SQLite Database
        public static async Task<List<Request>> GetAll()
        {
            List<Request> requests = new List<Request>();

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            
            requests = await conn.Table<Request>().ToListAsync();
            await conn.CloseAsync();
            
            return requests;
        }


        //This function sorts the requests by statusID
        public static async Task<List<Request>> SortByStatus(string searchText = null)
        {
            List<Request> requests = await GetAll();
            Status app = await Status.SearchStatuses("Approved");
            Status den = await Status.SearchStatuses("Denied");

            List<Request> sortedRequests = new List<Request>();


            if (searchText.Contains("Approved"))
            {
                 sortedRequests = requests.Where(g => g.StatusID != app.ID).ToList();
            }
            else if(searchText.Contains("Denied"))
            {
                 sortedRequests = requests.Where(g => g.StatusID != den.ID).ToList();
            }
            else
            {
                 sortedRequests = requests.Where(g => g.StatusID != app.ID
                                || g.StatusID != den.ID).ToList();
            }
            return sortedRequests;

        
            
        }

    }
}
