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
                UserID = messageViewModel.UserID,
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
                UserID = message.UserID,
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
            List<MessageViewModel> messageViewModel = new List<MessageViewModel>();
            messages = await App.MobileService.GetTable<Message>().ToListAsync();
            foreach (var item in messages)
            {
                var obj = ReturnMessageViewModel(item);
                if (obj.UserID != null)
                    obj.User = await UserViewModel.SearchByID(obj.UserID);
                if (obj.RequestID != null)
                    obj.Request = await RequestViewModel.SearchByID(obj.RequestID);
                messageViewModel.Add(obj);
            }
            
            return messageViewModel;
        }
        public static async Task<MessageViewModel> SearchByID(string ID)
        {
            List<MessageViewModel> messages = await GetAll();
            return messages.FirstOrDefault(u => u.ID == ID);

        }
        public static async Task<List<MessageViewModel>> SearchByUserID(string ID)
        {
            List<MessageViewModel> messages = await GetAll();
            return messages.Where(u => u.UserID.Contains(ID)).ToList();

        }
        public static async Task<List<MessageViewModel>> SearchByRequestID(string ID)
        {
            List<MessageViewModel> messages = await GetAll();
            return messages.Where(u => u.RequestID.Contains(ID)).ToList();

        }
    }
}
