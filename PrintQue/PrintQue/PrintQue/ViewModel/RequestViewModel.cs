using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
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
       
        public PrinterViewModel Printer { get; set; }
        public StatusViewModel Status { get; set; }
        public UserViewModel User { get; set; }
        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
        public static async Task Insert(RequestViewModel requestViewModel)
        {
            var request = ReturnRequest(requestViewModel);
            JObject jo = new JObject();
            jo.Add("PrinterId", requestViewModel.PrinterId);
            jo.Add("StatusId", requestViewModel.StatusId);
            jo.Add("ApplicationUserId", requestViewModel.ApplicationUserId);
            jo.Add("DateMade", DateTime.Now);
            jo.Add("DateRequested", requestViewModel.DateRequested);
            jo.Add("Duration", requestViewModel.Duration);
            jo.Add("ProjectName", requestViewModel.ProjectName);
            jo.Add("ProjectDescript", requestViewModel.ProjectDescript);
            jo.Add("ProjectFilePath", requestViewModel.ProjectFilePath);
            jo.Add("PersonalUse", requestViewModel.PersonalUse);
            await App.MobileService.GetTable<Request>().InsertAsync(jo);
            //await App.MobileService.SyncContext.PushAsync();
        }

        private static Request ReturnRequest(RequestViewModel requestViewModel)
        {
            var request = new Request()
            {
                Id                       = requestViewModel.Id,
                PrinterId                = requestViewModel.PrinterId,
                StatusId                 = requestViewModel.StatusId,
                ApplicationUserId        = requestViewModel.ApplicationUserId,
                DateMade                 = requestViewModel.DateMade,
                DateRequested            = requestViewModel.DateRequested,
                Duration                 = requestViewModel.Duration,
                ProjectName              = requestViewModel.ProjectName,
                ProjectDescript          = requestViewModel.ProjectDescript,
                ProjectFilePath          = requestViewModel.ProjectFilePath,
                PersonalUse              = requestViewModel.PersonalUse,

            };
            return request;
        }
        private static RequestViewModel ReturnRequestViewModel(Request request)
        {
            var requestViewModel = new RequestViewModel()
            {
                Id = request.Id,
                PrinterId = request.PrinterId,
                StatusId = request.StatusId,
                ApplicationUserId = request.ApplicationUserId,
                DateMade = request.DateMade,
                DateRequested = request.DateRequested,
                Duration = request.Duration,
                ProjectName = request.ProjectName,
                ProjectDescript = request.ProjectDescript,
                ProjectFilePath = request.ProjectFilePath,
                PersonalUse = request.PersonalUse,

            };
            return requestViewModel;
        }
        private static async Task<RequestViewModel> GetForeignKeys(RequestViewModel requestViewModel)
        {
            if (requestViewModel.PrinterId != null)
                requestViewModel.Printer = await PrinterViewModel.SearchByID(requestViewModel.PrinterId);
            if (requestViewModel.StatusId != null)
                requestViewModel.Status = await StatusViewModel.SearchByID(requestViewModel.StatusId);
            if (requestViewModel.ApplicationUserId != null)
                requestViewModel.User = await UserViewModel.SearchByID(requestViewModel.ApplicationUserId);
            if (requestViewModel.Id != null)
                requestViewModel.Messages = await MessageViewModel.SearchByRequestID(requestViewModel.Id);

            return requestViewModel;
        }
        //This function gets all requests in the SQLite Database
        public static async Task<List<RequestViewModel>> GetAll()
        {
            List<Request> requests = new List<Request>();
            List<RequestViewModel> requestviewmodel = new List<RequestViewModel>();
            requests = await App.MobileService.GetTable<Request>().ToListAsync();
            if (requests != null)
            {
                foreach (var req in requests)
                {
                    var request = ReturnRequestViewModel(req);
                    request = await GetForeignKeys(request);
                    requestviewmodel.Add(request);
                }
                return requestviewmodel;
            }
            else
            {
                return null;
            }
        }

        public static async Task Remove(RequestViewModel requestViewModel)
        {
            var request = ReturnRequest(requestViewModel);
            try
            {
                
                var messi = await App.MobileService.GetTable<Message>().Where(m => m.RequestId == request.Id).ToListAsync();
                
                foreach(var item in messi)
                {
                    await App.MobileService.GetTable<Message>().DeleteAsync(item);
                }
                await App.MobileService.GetTable<Request>().DeleteAsync(request);
                //await App.MobileService.SyncContext.PushAsync();
            }
            catch (Exception)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", "Failed to Delete", "OK");
            }
        }

        public static async Task<int> Update(RequestViewModel requestViewModel)
        {
            var request = ReturnRequest(requestViewModel);
            var test = await SearchByID(requestViewModel.Id);
            if (test != null)
            {
                await App.MobileService.GetTable<Request>().UpdateAsync(request);
                //await App.MobileService.SyncContext.PushAsync();

                return 1;
            }
            else
            {
                return 0;
            }

        }
        private static List<RequestViewModel> ReturnListRequestViewModel(List<Request> list)
        {
            List<RequestViewModel> trans = new List<RequestViewModel>();
            foreach (var item in list)
            {
                trans.Add(ReturnRequestViewModel(item));
            }
            return trans;
        }
        //This function sorts the requests by statusID
        public static async Task<List<RequestViewModel>> SortByStatus(string searchText = null)
        {
            var status = await StatusViewModel.SearchByName(searchText);
            List<Request> sortedRequests = await App.MobileService.GetTable<Request>().Where(sr => sr.StatusId.Contains(status.ID)).ToListAsync();
            if (sortedRequests != null)
            {
                return ReturnListRequestViewModel(sortedRequests);
            }
            else
            {
                return null;
            }

        }

        //Useless??
        public static async Task<RequestViewModel> SearchByName(string searchText = null)
        {
            Request sortedRequests = (await App.MobileService.GetTable<Request>().Where(sr => sr.ProjectName.Contains(searchText)).ToListAsync()).FirstOrDefault();
            if (sortedRequests != null)
            {
                return ReturnRequestViewModel(sortedRequests);
            }
            else
            {
                return null;
            }

        }
        public static async Task<List<RequestViewModel>> SearchByPrinter(PrinterViewModel printerViewModel)
        {
            List<Request> sortedRequests = (await App.MobileService.GetTable<Request>().Where(sr => sr.PrinterId.Contains(printerViewModel.ID)).ToListAsync());
            if (sortedRequests != null)
            {
                return ReturnListRequestViewModel(sortedRequests);
            }
            else
            {
                return null;
            }


        }

        public static async Task<List<RequestViewModel>> SearchByUser(string ID)
        {
            var requests = ReturnListRequestViewModel(await App.MobileService.GetTable<Request>().Where(r => r.ApplicationUserId.Contains(ID)).ToListAsync());
            var temp = new List<RequestViewModel>();
            if (requests != null)
            {


                return requests;
            }
            else
            {
                return null;
            }


        }
        public static async Task<RequestViewModel> SearchByID(string ID)
        {
            Request sortedRequests = (await App.MobileService.GetTable<Request>().Where(sr => sr.Id.Contains(ID)).ToListAsync()).FirstOrDefault();
            if (sortedRequests != null)
            {

                return ReturnRequestViewModel(sortedRequests);
            }
            else
            {
                return null;
            }

        }
        public static async Task<RequestViewModel> SearchProjectNameByUser(RequestViewModel requestViewModel)
        {
            var sortedRequests = (await App.MobileService.GetTable<Request>().Where(r => r.ApplicationUserId == requestViewModel.ApplicationUserId && r.ProjectName.Contains(requestViewModel.ProjectName)).ToListAsync()).FirstOrDefault();

            if (sortedRequests != null)
            {

                return ReturnRequestViewModel(sortedRequests);
            }
            else
            {
                return null;
            }

        }
    }
}
