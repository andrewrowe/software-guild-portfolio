using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.TestRepositories
{
    public class TestMessagesRepository : IMessagesRepository
    {
        private static List<Message> _messages = new List<Message>
        {
            new Message {Body = "Hello there!", Id = 1, IsRead = true, RecipientUserId = "5", SendUserId = "1", SendDate = DateTime.Parse("4/20/1990"), Subject = "Hi"},
            new Message {Body = "We're expanding!", Id = 2, IsRead = true, RecipientUserId = "16", SendUserId = "2", SendDate = DateTime.Parse("5/15/16"), Subject = "Big News!"},
            new Message {Body = "Issue has been resolved.", Id = 3, IsRead = true, RecipientUserId = "1", SendUserId = "2", SendDate = DateTime.Parse("7/2/16"), Subject = "Status Update"},
            new Message {Body = "About giving me a raise? :)", Id = 4, IsRead = false, RecipientUserId = "1", SendUserId = "4", SendDate = DateTime.Today, Subject = "What do you think?"}
        };

        public List<Message> GetAllForUser(string userId)
        {
            var messages = _messages.Where(m => m.RecipientUserId == userId || m.SendUserId == userId).ToList();
            return messages;
        }

        public Message Get(int messageId)
        {
            return _messages.FirstOrDefault(m => m.Id == messageId);
        }

        public Message Insert(Message message)
        {
            _messages.Add(message);
            return message;
        }

        public void Delete(int messageId)
        {
            var removeMessage = _messages.FirstOrDefault(m => m.Id == messageId);
            _messages.Remove(removeMessage);
        }

        public void Update(Message message)
        {
            var updateMessage = _messages.FirstOrDefault(m => m.Id == message.Id);
            updateMessage.Body = message.Body;
            updateMessage.Id = message.Id;
            updateMessage.IsRead = message.IsRead;
            updateMessage.RecipientUserId = message.RecipientUserId;
            updateMessage.SendDate = message.SendDate;
            updateMessage.Subject = message.Subject;
            updateMessage.SendUserId = message.SendUserId;
        }
    }
}
