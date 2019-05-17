using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.WindowsAzure.MobileServices;
using MimeKit;
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

        public static async void PostMessage(string body, UserViewModel user = null)
        {
            var newMsg = new MessageViewModel()
            {
                Body = body,
                Sender = await UserViewModel.SearchByID(App.LoggedInUserID),
                SenderID = App.LoggedInUserID,
            };
            var message = new MimeMessage();
            message.Subject = "AUTOMATED MESSAGE - DO NOT REPLY";
            if (user == null)
            {
                message.From.Add(new MailboxAddress(string.Concat(newMsg.Sender.FirstName + " " + newMsg.Sender.LastName), newMsg.Sender.Email));
                message.To.Add(new MailboxAddress(string.Concat(newMsg.Sender.FirstName + " " + newMsg.Sender.LastName), "THEPRE.S.Q.L@gmail.com"));

            }
            else
            {

                message.From.Add(new MailboxAddress(string.Concat(newMsg.Sender.FirstName + " " + newMsg.Sender.LastName), "OregonTech3DPrintClub@donotreply.com"));
                message.To.Add(new MailboxAddress(string.Concat(user.FirstName + " " + user.LastName), user.Email));

            }
            var builder = new BodyBuilder();
            builder.TextBody = body;
            message.Body = builder.ToMessageBody();
            try
            {
                var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("THEPRE.S.Q.L@gmail.com", "CST3162018");
                client.Send(message);
                client.Disconnect(true);

                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Success!", "You have successfully sent email!", "OK");

            }
            catch (Exception e)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failure!", "You have successfully failed at everything! Go die!", "OK");

            }

            //SmtpClient client = new SmtpClient
            //{
            //    Port = 587,
            //    Host = "smtp.gmail.com",
            //    EnableSsl = true,
            //    Timeout = 10000,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new System.Net.NetworkCredential("THEPRE.S.Q.L@gmail.com", "CST3162018")
            //};
            //var userToEmail = await UserViewModel.SearchByID(App.LoggedInUserID);
            ////at the moment the users name is set as their email
            //MailMessage message = new MailMessage("OregonTech3DPrintClub@donotreply.com", userToEmail.Email, "3D Print Club", newMsg.Body.ToString())
            //{
            //    BodyEncoding = Encoding.UTF8,
            //    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            //};

            //client.Send(message);


            //newMsg.Sender = userToEmail;
            //newMsg.TimeSent = DateTime.Now;
            //newMsg.Request = (await RequestViewModel.SearchByUser(App.LoggedInUserID)).OrderBy(d => d.DateMade).FirstOrDefault();

            //await Insert(newMsg);

        }

        private static Message ReturnMessage(MessageViewModel messageViewModel)
        {
            var messi = new Message()
            {
                ID = messageViewModel.ID,
                SenderID = messageViewModel.SenderID,
                Body = messageViewModel.Body,
                RequestID = messageViewModel.RequestID,
                TimeSent = messageViewModel.TimeSent,
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
                TimeSent = message.TimeSent,
            };
            return messi;
        }
        public static async Task Insert(MessageViewModel messageviewmodel)
        {
            var messi = ReturnMessage(messageviewmodel);
            await App.MobileService.GetTable<Message>().InsertAsync(messi);
            //await App.MobileService.SyncContext.PushAsync();
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
                obj.Sender = await UserViewModel.SearchByID(obj.SenderID);
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
