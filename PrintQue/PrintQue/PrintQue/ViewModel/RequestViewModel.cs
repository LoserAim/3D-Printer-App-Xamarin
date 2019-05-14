using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class RequestViewModel : Request
    {
       
        public Printer Printer { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
        public static async Task Insert(RequestViewModel requestViewModel)
        {
            var request = ReturnRequest(requestViewModel);
            await App.MobileService.GetTable<Request>().InsertAsync(request);
        }

        private static Request ReturnRequest(RequestViewModel requestViewModel)
        {
            var request = new Request()
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
            return request;
        }
        private static RequestViewModel ReturnRequestViewModel(Request request)
        {
            var requestViewModel = new RequestViewModel()
            {
                ID = request.ID,
                PrinterID = request.PrinterID,
                StatusID = request.StatusID,
                UserID = request.UserID,
                DateMade = request.DateMade,
                DateRequested = request.DateRequested,
                Duration = request.Duration,
                ProjectName = request.ProjectName,
                Description = request.Description,
                File = request.File,
                Personal = request.Personal,

            };
            return requestViewModel;
        }
        private static async Task<RequestViewModel> GetForeignKeys(RequestViewModel requestViewModel)
        {
            if (requestViewModel.PrinterID != null)
                requestViewModel.Printer = await PrinterViewModel.SearchByID(requestViewModel.PrinterID);
            if (requestViewModel.StatusID != null)
                requestViewModel.Status = await StatusViewModel.SearchByID(requestViewModel.StatusID);
            if (requestViewModel.UserID != null)
                requestViewModel.User = await UserViewModel.SearchByID(requestViewModel.UserID);
            if (requestViewModel.ID != null)
                requestViewModel.Messages = await MessageViewModel.SearchByRequestID(requestViewModel.ID);

            return requestViewModel;
        }
        //This function gets all requests in the SQLite Database
        public static async Task<List<RequestViewModel>> GetAll()
        {
            List<Request> requests = new List<Request>();
            List<RequestViewModel> requestviewmodel = new List<RequestViewModel>();
            requests = await App.MobileService.GetTable<Request>().ToListAsync();
            foreach (var req  in requests)
            {
                var request = ReturnRequestViewModel(req);
                request = await GetForeignKeys(request);
                requestviewmodel.Add(request);
            }
            return requestviewmodel;
        }

        public static async Task Remove(RequestViewModel requestViewModel)
        {
            var request = ReturnRequest(requestViewModel);
            await App.MobileService.GetTable<Request>().DeleteAsync(request);
        }

        public static async Task<int> Update(RequestViewModel requestViewModel)
        {
            var request = ReturnRequest(requestViewModel);
            var test = await SearchByID(requestViewModel.ID);
            if (test != null)
            {
                await App.MobileService.GetTable<Request>().UpdateAsync(request);
                return 1;
            }
            else
            {
                return 0;
            }

        }
        //This function sorts the requests by statusID
        public static async Task<List<RequestViewModel>> SortByStatus(string searchText = null)
        {
            List<RequestViewModel> requests = await GetAll();
            var status = await StatusViewModel.SearchByName(searchText);
            List<RequestViewModel> sortedRequests = requests.Where(r => r.StatusID.Contains(status.ID)).ToList();
            return sortedRequests;

        }
        public static async Task<RequestViewModel> SearchByName(string searchText = null)
        {
            List<RequestViewModel> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.ProjectName.Contains(searchText));
            return sortedRequests;

        }
        public static async Task<List<RequestViewModel>> SearchByPrinter(PrinterViewModel printerViewModel)
        {
            List<RequestViewModel> requests = await GetAll();
            var sortedRequests = requests.Where(r => r.PrinterID.Contains(printerViewModel.ID)).ToList();
            return sortedRequests;

        }
        public static async Task<RequestViewModel> SearchByID(string ID)
        {
            List<RequestViewModel> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.ID.Contains(ID));
            return sortedRequests;

        }
        public static async Task<RequestViewModel> SearchProjectNameByUser(RequestViewModel requestViewModel)
        {
            List<RequestViewModel> requests = await GetAll();
            var sortedRequests = requests.FirstOrDefault(r => r.UserID == requestViewModel.UserID && r.ProjectName.Contains(requestViewModel.ProjectName));
            return sortedRequests;

        }
    }
}
