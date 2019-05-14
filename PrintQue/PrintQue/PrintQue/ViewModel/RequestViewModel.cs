using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class RequestViewModel : Request
    {
       
        public Printer Printer { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public static async Task Insert(RequestViewModel requestViewModel)
        {
            var request = new Request()
            {
                ID              = requestViewModel.ID,
                PrinterID       = requestViewModel.PrinterID,
                StatusID        = requestViewModel.StatusID,    
                UserID          = requestViewModel.UserID,       
                DateMade        = requestViewModel.DateMade,     
                DateRequested   = requestViewModel.DateRequested,
                Duration        = requestViewModel.Duration,     
                ProjectName     = requestViewModel.ProjectName,
                Description     = requestViewModel.Description,  
                File            = requestViewModel.File,         
                Personal        = requestViewModel.Personal,     

            };
            await App.MobileService.GetTable<Request>().InsertAsync(request);
        }



        //This function gets all requests in the SQLite Database
        public static async Task<List<Request>> GetAll()
        {
            List<Request> requests = new List<Request>();
            List<RequestViewModel> usersviewmodel = new List<RequestViewModel>();
            users = await App.MobileService.GetTable<User>().ToListAsync();
            foreach (var u in users)
            {
                var request = new RequestViewModel()
                {
                    ID = requestViewModel.ID,
                    PrinterID = requestViewModel.PrinterID,
                    StatusID = requestViewModel.StatusID,
                    UserID = requestViewModel.UserID,
                    DateMade = requestViewModel.DateMade,
                    DateRequested = requestViewModel.DateRequested,
                    Duration = requestViewModel.Duration,
                    ProjectName = requestViewModel.ProjectName,
                    Description = requestViewModel.Description,
                    File = requestViewModel.File,
                    Personal = requestViewModel.Personal,

                };
                usersviewmodel.Add(inser);
            }
            //ADD PULL OF FOREIGN KEYS
            return usersviewmodel;
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
            if (test != null)
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
