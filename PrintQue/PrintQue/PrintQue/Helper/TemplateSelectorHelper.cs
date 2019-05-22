
using PrintQue.ChatTemplates;
using PrintQue.ViewModel;


using Xamarin.Forms;
namespace PrintQue.Helper
{
    public class TemplateSelectorHelper : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;
        public TemplateSelectorHelper()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as MessageViewModel;
            if (messageVm == null)
                return null;
            messageVm.Sender = App.LoggedInUser;
            
            return (messageVm.SenderId == App.LoggedInUser.ID) ? incomingDataTemplate : outgoingDataTemplate;
        }
    }
}
