using PrintQue.Models;
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
	public partial class AdminMessagesPage : ContentPage
	{
        MessageViewModel messageViewModel;
        public AdminMessagesPage ()
		{
            InitializeComponent();
            messageViewModel = new MessageViewModel();
            BindingContext = messageViewModel;
        }

        private void sendButton_Clicked(object sender, EventArgs e)
        {
            Message message = new Message();
            message.Sender = "replace@this.com";
            message.Body = messageEntry.Text;
            messageViewModel.messages.Add(message);
        }
    }
}