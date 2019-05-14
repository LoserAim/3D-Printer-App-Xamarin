using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class RequestsViewModel : Request
    {
        public ObservableCollection<RequestViewModel> Requests { get; set; } = new ObservableCollection<RequestViewModel>();

        public async Task RefreshRequests()
        {
            var req = await RequestViewModel.GetAll();
            if(req != null)
            {
                Requests.Clear();
                foreach (var request in req)
                {
                    Requests.Add(request);
                }
            }
            

        }

        public async void UpdateRequests(string _searchFilter = null)
        {

            var req = await RequestViewModel.GetAll();
            if(req != null)
            {
                Requests.Clear();
                if (_searchFilter.Contains("Pending"))
                {

                    foreach (var request in req.Where(s => s.Status.Name.Contains("nostatus")))
                    {
                        Requests.Add(request);
                    }
                }
                else if (!_searchFilter.Contains("All"))
                {
                    foreach (var request in req.Where(s => s.Status.Name.Contains(_searchFilter)))
                    {
                        Requests.Add(request);
                    }

                }
                else
                {
                    foreach (var request in req)
                    {
                        Requests.Add(request);
                    }
                }
            }
        }

        public async void SearchRequests(string _searchFilter = null)
        {


            var req = await RequestViewModel.GetAll();

            if (req != null)
            {
                Requests.Clear();
                if (_searchFilter != null)
                {

                    foreach (var request in req.Where(r => r.ProjectName.Contains(_searchFilter)
                    || r.User.FirstName.Contains(_searchFilter)))
                    {
                        Requests.Add(request);
                    }
                }
                else
                {
                    foreach (var request in req)
                    {
                        Requests.Add(request);
                    }
                }
            }
        }
    }
}
