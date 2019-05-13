using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class MessageViewModel : Message
    {
        public User User { get; set; }
        public Request Request { get; set; }

        public static async Task<int> Insert(MessageViewModel messageviewmodel)
        {
            var messi = new Message()
            {
                UserID = messageviewmodel.UserID,
                Body = messageviewmodel.Body,
                RequestID = messageviewmodel.RequestID,
                Sent = messageviewmodel.Sent,
            };
            await App.MobileService.GetTable<Message>().InsertAsync(messi);

        }
        public static async Task<List<Message>> GetAll()
        {
            List<Message> messages = new List<Message>();

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            messages = await conn.GetAllWithChildrenAsync<Message>();


            return messages;
        }
    }
}
