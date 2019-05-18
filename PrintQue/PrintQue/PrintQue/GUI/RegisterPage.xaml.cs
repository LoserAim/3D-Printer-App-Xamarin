using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrintQue.ViewModel;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RegisterViewModel viewModel;
        public RegisterPage()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            BindingContext = viewModel;
        }


    }
}