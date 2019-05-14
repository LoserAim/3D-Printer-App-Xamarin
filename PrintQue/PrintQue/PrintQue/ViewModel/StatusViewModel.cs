using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class StatusViewModel : Status
    {

        public List<PrinterViewModel> Printers { get; set; } = new List<PrinterViewModel>();
        public List<RequestViewModel> Requests { get; set; } = new List<RequestViewModel>();


        private static Status ReturnStatus(StatusViewModel statusViewModel)
        {
            var status = new Status()
            {
                ID = statusViewModel.ID,
                Name = statusViewModel.Name,
   
            };
            return status;
        }
        private static StatusViewModel ReturnStatusViewModel(Status status)
        {
            var statusViewModel = new StatusViewModel()
            {
                ID = status.ID,
                Name = status.Name,

            };
            return statusViewModel;
        }

        public static async Task Insert(StatusViewModel statusViewModel)
        {
            var status = ReturnStatus(statusViewModel);
            await App.MobileService.GetTable<Status>().InsertAsync(status);
        }
        public static async Task<List<StatusViewModel>> GetAll()
        {
            List<Status> statuses = new List<Status>();
            List<StatusViewModel> statusViewModels = new List<StatusViewModel>();
            statuses = await App.MobileService.GetTable<Status>().ToListAsync();
            foreach (var item in statuses)
            {
                var status = ReturnStatusViewModel(item);
                statusViewModels.Add(status);
            }
            //ADD PULL OF FOREIGN KEYS
            return statusViewModels;
        }
        public static async Task<StatusViewModel> SearchByName(string searchText = null)
        {
            List<StatusViewModel> statuses = await GetAll();

            return statuses.FirstOrDefault(g => g.Name == searchText);
        }
        public static async Task<StatusViewModel> SearchByID(string ID)
        {
            List<StatusViewModel> statuses = await GetAll();

            return statuses.FirstOrDefault(g => g.ID == ID);
        }

    }
}
