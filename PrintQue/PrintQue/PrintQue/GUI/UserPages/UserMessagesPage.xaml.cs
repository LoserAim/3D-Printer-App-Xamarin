using PrintQue.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMessagesPage : ContentPage
    {
        private bool isDataLoaded = false;
        public UserMessagesPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            if (isDataLoaded)
                return;
            isDataLoaded = true;
            MessageViewModel.PostMessage("KONOWADIODESU");
            base.OnAppearing();

        }
    }
}