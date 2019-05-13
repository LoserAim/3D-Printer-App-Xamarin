using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

using System.Threading.Tasks;
using System.Linq;


namespace PrintQue.Models
{
    public class Request 
    {
        [PrimaryKey]
        public string ID { get; set; }
        [ForeignKey(typeof(Printer))]
        public string PrinterID { get; set; }
        [ManyToOne]
        public Printer Printer { get; set; }
        [ForeignKey(typeof(Status))]
        public string StatusID { get; set; }
        [ManyToOne]
        public Status Status { get; set; }
        [ForeignKey(typeof(User))]
        public string UserID { get; set; }
        [ManyToOne]
        public User User { get; set; }
        public DateTime DateMade { get; set; }
        public DateTime DateRequested { get; set; }
        public int Duration { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Personal { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Message> Messages { get; set; } = new List<Message>();
        public static async Task<int> Insert(Request request)
        {

            var status = request.Status;
            var user = request.User;
            var printer = request.Printer;
            status.Requests.Add(request);
            user.Requests.Add(request);
            printer.Requests.Add(request);
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(request);
            await conn.UpdateWithChildrenAsync(status);
            await conn.UpdateWithChildrenAsync(printer);
            await conn.UpdateWithChildrenAsync(user);


            return rows;
            
        }



        //This function gets all requests in the SQLite Database
        public static async Task<List<Request>> GetAll()
        {
            List<Request> requests = new List<Request>();
            
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            requests = await conn.GetAllWithChildrenAsync<Request>();


            return requests;
        }

        public static async Task<int> Remove(Request request)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            var num = await conn.DeleteAsync<Request>(request.ID);
            return num;
        }

        public static async Task<int> Update(Request request)
        {
            var test = await SearchByID(request);
            if(test != null)
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
                await conn.UpdateWithChildrenAsync(request);
                return 1;
            }
            else
            {
                return 0;
            }

        }
        //This function sorts the requests by statusID
        public static async Task<List<Request>> SortByStatus(string searchText = null)
        {
            List<Request> requests = await GetAll();
            var status = await Status.SearchByName(searchText);
            List<Request> sortedRequests = requests.Where(r => r.StatusID.Contains(status.ID)).ToList();
            return sortedRequests;
            
        }
        public static async Task<Request> SearchByName(string searchText = null)
        {
            List<Request> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.ProjectName.Contains(searchText));
            return sortedRequests;

        }
        public static async Task<Request> SearchByID(Request request)
        {
            List<Request> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.ID == request.ID);
            return sortedRequests;

        }
        public static async Task<Request> SearchProjectNameByUser(Request request)
        {
            List<Request> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.UserID == request.UserID && r.ProjectName.Contains(request.ProjectName));
            return sortedRequests;

        }

    }
}
