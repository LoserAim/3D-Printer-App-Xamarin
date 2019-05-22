using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrintQue.ViewModel
{
    public class UserSubmissionsViewModel
    {
        public ObservableCollection<RequestViewModel> requests { get; set; } = new ObservableCollection<RequestViewModel>();

        public UserSubmissionsViewModel()
        {
            //UpdateRequestsList();
        }
        public async void UpdateRequestsList()
        {
            
            var temp = await RequestViewModel.SearchByUser(App.LoggedInUser.ID);

            if (temp != null)
            {
                requests.Clear();
                foreach (var req in temp)
                {
                    if (req.PrinterId != null)
                        req.Printer = await PrinterViewModel.SearchByID(req.PrinterId);
                    if (req.StatusId != null)
                        req.Status = await StatusViewModel.SearchByID(req.StatusId);
                    if (req.ApplicationUserId != null)
                        req.User = await UserViewModel.SearchByID(req.ApplicationUserId);
                    if (req.ID != null)
                        req.Messages = await MessageViewModel.SearchByRequestID(req.ID);
                    requests.Add(req);
                }
            }
        }

    }
}
