using PrintQue.ViewModel;
using PrintQue.Widgets.ChatWidget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.Views.Partials
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatInputBarView : ContentView
	{
        public ChatInputBarView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, chatTextInput));
            }
        }
        public void Handle_Completed(object sender, EventArgs e)
        {
            (this.Parent.Parent.BindingContext as ChatRoomViewModel).OnSendCommand.Execute(null);
            chatTextInput.Focus();
            (this.Parent.Parent as ChatPage).ScrollListCommand.Execute(null);
        }

        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
        }
    }
}