using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;
using SQLiteNetExtensionsAsync.Extensions;

namespace PrintQue.Models
{
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(User))]
        public int UserID { get; set; }
        [ManyToOne]
        public User User { get; set; }
        [ForeignKey(typeof(Request))]
        public int RequestID { get; set; }
        [ManyToOne]
        public Request Request { get; set; }
        public DateTime Sent { get; set; }
        public string Body { get; set; }

        public static async Task<int> Insert(Message message)
        {


            var user = message.User;
            var request = message.Request;
            user.Messages.Add(message);
            request.Messages.Add(message);
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(message);
            await conn.UpdateWithChildrenAsync(request);
            await conn.UpdateWithChildrenAsync(user);


            return rows;



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

