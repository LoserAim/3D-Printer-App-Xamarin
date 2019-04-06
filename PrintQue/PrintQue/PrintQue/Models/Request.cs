using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;
using Newtonsoft.Json;
<<<<<<< HEAD
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
=======
>>>>>>> parent of e8b7215... Implementing async features

namespace PrintQue.Models
{
    public class Request 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(Printer))]
        public int PrinterID { get; set; }
        [ForeignKey(typeof(Status))]
        public int StatusID { get; set; }
        [ForeignKey(typeof(User))]
        public int UserID { get; set; }
        public DateTime DateMade { get; set; }
        public DateTime DateRequested { get; set; }
        public int Duration { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Personal { get; set; }
<<<<<<< HEAD
        public User user { get; set; }
        public Status status { get; set; }
        public Printer printer { get; set; }
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
                
                return rows;
            }
        }

        public static async Task<int> Update(Request request)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.UpdateAsync(request);
            
            return rows;
        }

        //This function gets all requests in the SQLite Database
        public static async Task<List<Request>> GetAll()
        {
            List<Request> requests = new List<Request>();
            
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            await conn.CreateTableAsync<Request>();
            requests = await conn.Table<Request>().ToListAsync();
            foreach (Request p in requests)
            {
                p.user = await User.SearchByID(p.UserID);
                p.printer = await Printer.SearchByID(p.PrinterID);
                p.status = await Status.SearchByID(p.StatusID);
            }

            return requests;
        }
        public static async Task<User> GetChildUser(Request request)
        {
            return await User.SearchByID(request.UserID);
        }

        public static async Task<Status> GetChildStatus(Request request)
        {
            return await Status.SearchByID(request.StatusID);
        }

        public static async Task<Printer> GetChildPrinter(Request request)
        {
            return await Printer.SearchByID(request.PrinterID);
        }

        //This function sorts the requests by statusID
        public static async Task<List<Request>> SortByStatus(string searchText = null)
        {
            List<Request> requests = await GetAll();
            var status = Status.SearchByName(searchText);
            List<Request> sortedRequests = requests.Where(r => r.StatusID == status.Id).ToList();
            return sortedRequests;
            
        }
        public static async Task<Request> SearchByName(string searchText = null)
        {
            List<Request> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.ProjectName.Contains(searchText));
            return sortedRequests;

        }

=======
        [ManyToOne]
        public User user { get; set; }
        [ManyToOne]
        public Status status { get; set; }
        [ManyToOne]
        public Printer printer { get; set; }
>>>>>>> parent of e8b7215... Implementing async features
    }
}
