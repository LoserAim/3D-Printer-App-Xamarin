using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PrintQue.ViewModel
{
    public class RequestsViewModel
    {
        public ObservableCollection<Request> Requests { get; set; }

        public RequestsViewModel()
        {
            Requests = new ObservableCollection<Request>();
        }

        public async void UpdateRequests(string _searchFilter = null)
        {

            var req = await Request.GetAll();
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


            var req = await Request.GetAll();

            if (req != null)
            {
                Requests.Clear();
                if (_searchFilter != null)
                {

                    foreach (var request in req.Where(r => r.ProjectName.Contains(_searchFilter)
                    || r.User.Name.Contains(_searchFilter)))
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
