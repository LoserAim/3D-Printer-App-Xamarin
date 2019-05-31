using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.WindowsAzure.MobileServices;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public UserViewModel Sender { get; set; }
        public RequestViewModel Request { get; set; }

        public static void PostMessage(MessageViewModel newMsg, UserViewModel user = null)
        {

            var message = new MimeMessage();
            message.Subject = "AUTOMATED MESSAGE - DO NOT REPLY";
            if (user == null)
            {
                message.From.Add(new MailboxAddress(string.Concat(newMsg.Sender.First_Name + " " + newMsg.Sender.Last_Name), newMsg.Sender.Email));
                message.To.Add(new MailboxAddress(string.Concat(newMsg.Sender.First_Name + " " + newMsg.Sender.Last_Name), "THEPRE.S.Q.L@gmail.com"));

            }
            else
            {

                message.From.Add(new MailboxAddress(string.Concat(newMsg.Sender.First_Name + " " + newMsg.Sender.Last_Name), "OregonTech3DPrintClub@donotreply.com"));
                message.To.Add(new MailboxAddress(string.Concat(user.First_Name + " " + user.Last_Name), user.Email));

            }
            var builder = new BodyBuilder();
            builder.TextBody = newMsg.Body;
            message.Body = builder.ToMessageBody();
            try
            {
                var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("THEPRE.S.Q.L@gmail.com", "CST3162018");
                client.Send(message);
                client.Disconnect(true);


            }
            catch (Exception e)
            {

            }



        }

        private static Message ReturnMessage(MessageViewModel messageViewModel)
        {

            var messi = new Message()
            {
                ID = messageViewModel.ID,
                SenderId = messageViewModel.SenderId,
                Body = messageViewModel.Body,
                RequestId = messageViewModel.RequestId,
                TimeSent = messageViewModel.TimeSent,
            };
            return messi;
        }
        private static MessageViewModel ReturnMessageViewModel(Message message)
        {
            var messi = new MessageViewModel()
            {
                ID = message.ID,
                SenderId = message.SenderId,
                Body = message.Body,
                RequestId = message.RequestId,
                TimeSent = message.TimeSent,
            };
            return messi;
        }
        public static async Task Insert(MessageViewModel messageviewmodel)
        {
            if (App.LoggedInUser.Admin == 1)
                PostMessage(messageviewmodel, messageviewmodel.Request.User);
            else
                PostMessage(messageviewmodel);
            var messi = ReturnMessage(messageviewmodel);
            JObject jo = new JObject();
            jo.Add("SenderId", messi.SenderId);
            jo.Add("Body", messi.Body);
            jo.Add("RequestId", messi.RequestId);
            jo.Add("TimeSent", DateTime.Now);
            await App.MobileService.GetTable<Message>().InsertAsync(jo);

            //await App.MobileService.SyncContext.PushAsync();
        }
        public static async Task<List<MessageViewModel>> GetAll()
        {
            List<Message> messages = new List<Message>();
            
            messages = await App.MobileService.GetTable<Message>().ToListAsync();
            if(messages == null)
                return null;
            
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
            if (obj.SenderId != null)
                obj.Sender = await UserViewModel.SearchByID(obj.SenderId);
            if (obj.RequestId != null)
                obj.Request = await RequestViewModel.SearchByID(obj.RequestId);
            return obj;
        }
        public static async Task<MessageViewModel> SearchByID(string ID)
        {
            Message sorted = (await App.MobileService.GetTable<Message>().Where(u => u.ID.Contains(ID)).ToListAsync()).FirstOrDefault();
            return ReturnMessageViewModel(sorted);
        }
        public static async Task<List<MessageViewModel>> SearchByUserID(string ID)
        {
            List<Message> sorted = (await App.MobileService.GetTable<Message>().Where(u => u.SenderId.Contains(ID)).ToListAsync());
            return ReturnListMessageViewModel(sorted);

        }
        public static async Task<List<MessageViewModel>> SearchByRequestID(string ID)
        {
            List<Message> sorted = (await App.MobileService.GetTable<Message>().Where(u => u.RequestId.Contains(ID)).ToListAsync());
            return ReturnListMessageViewModel(sorted);
        }
    }
}
