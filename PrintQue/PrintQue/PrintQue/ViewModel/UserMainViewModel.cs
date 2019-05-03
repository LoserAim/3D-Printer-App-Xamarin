using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.ViewModel
{
    public class UserMainViewModel
    {
        public NavigationCommand NavCommand { get; set; }

        public UserMainViewModel()
        {
            NavCommand = new NavigationCommand(this);
        }

        public void Navigate()
        {
            Request request = null;
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RequestDetailPage(request, 1));
        }
    }
}
