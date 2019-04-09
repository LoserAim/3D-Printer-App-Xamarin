using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        MessageViewModel messageViewModel;
        public UserMessagesPage()
        {
            InitializeComponent();
            messageViewModel = new MessageViewModel();
            BindingContext = messageViewModel;
        }

        private void sendButton_Clicked(object sender, EventArgs e)
        {
            Message message = new Message();
            message.sender = "replace@this.com";
            message.body = messageEntry.Text;
            messageViewModel.messages.Add(message);
        }
    }
}