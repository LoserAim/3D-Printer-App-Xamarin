using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class MessageViewModel : Message
    {
        public UserViewModel User { get; set; }
        public RequestViewModel Request { get; set; }



        private static Message ReturnMessage(MessageViewModel messageViewModel)
        {
            var messi = new Message()
            {
                ID = messageViewModel.ID,
                SenderID = messageViewModel.SenderID,
                Body = messageViewModel.Body,
                RequestID = messageViewModel.RequestID,
                Sent = messageViewModel.Sent,
            };
            return messi;
        }
        private static MessageViewModel ReturnMessageViewModel(Message message)
        {
            var messi = new MessageViewModel()
            {
                ID = message.ID,
                SenderID = message.SenderID,
                Body = message.Body,
                RequestID = message.RequestID,
                Sent = message.Sent,
            };
            return messi;
        }
        public static async Task Insert(MessageViewModel messageviewmodel)
        {
            var messi = ReturnMessage(messageviewmodel);
            await App.MobileService.GetTable<Message>().InsertAsync(messi);
        }
        public static async Task<List<MessageViewModel>> GetAll()
        {
            List<Message> messages = new List<Message>();
            messages = await App.MobileService.GetTable<Message>().ToListAsync();

            
            return ReturnListMessageViewModel(messages);
        }

        private static List<MessageViewModel> ReturnListMessageViewModel(List<Message> messages)
        {
            List<MessageViewModel> messageViewModel = new List<MessageViewModel>();
            foreach (var item in messages)
            {
                var obj = ReturnMessageViewModel(item);

                messageViewModel.Add(obj);
            }
            return messageViewModel;
        }
        public static async  Task<MessageViewModel> GetForeignKeys(MessageViewModel obj)
        {
            if (obj.SenderID != null)
                obj.User = await UserViewModel.SearchByID(obj.SenderID);
            if (obj.RequestID != null)
                obj.Request = await RequestViewModel.SearchByID(obj.RequestID);
            return obj;
        }
        public static async Task<MessageViewModel> SearchByID(string ID)
        {
            Message sorted = (await App.MobileService.GetTable<Message>().Where(u => u.ID.Contains(ID)).ToListAsync()).FirstOrDefault();
            return ReturnMessageViewModel(sorted);
        }
        public static async Task<List<MessageViewModel>> SearchByUserID(string ID)
        {
            List<Message> sorted = (await App.MobileService.GetTable<Message>().Where(u => u.SenderID.Contains(ID)).ToListAsync());
            return ReturnListMessageViewModel(sorted);

        }
        public static async Task<List<MessageViewModel>> SearchByRequestID(string ID)
        {
            List<Message> sorted = (await App.MobileService.GetTable<Message>().Where(u => u.RequestID.Contains(ID)).ToListAsync());
            return ReturnListMessageViewModel(sorted);
        }
    }
}
