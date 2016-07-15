using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.BLL
{
    public class UserManager
    {
        private readonly IMessagesRepository _messageRepo;
        private readonly ICategoriesRepository _categoryRepo;

        public UserManager()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _messageRepo = kernel.Get<IMessagesRepository>();
            _categoryRepo = kernel.Get<ICategoriesRepository>();
        }

        public List<Message> GetAllSendMessages(string userId)
        {
            return _messageRepo.GetAllForUser(userId).Where(m => m.SendUserId == userId && !m.IsSenderDeleted).ToList();
        }

        public List<Message> GetAllReceiveMessages(string userId)
        {
            return _messageRepo.GetAllForUser(userId).Where(m => m.RecipientUserId == userId && !m.IsReceiverDeleted).ToList();
        }

        public int CountNoReadMessages(string userId)
        {
            return _messageRepo.GetAllForUser(userId).Count(m => m.RecipientUserId == userId && m.IsRead == false && !m.IsReceiverDeleted);
        }

        public Message CreateMessage(Message message)
        {
            return _messageRepo.Insert(message);
        }

        public Message Get1Message(int messageId)
        {
            return _messageRepo.Get(messageId);
        }

        public void PutMessageAsRead(Message message)
        {
            message.IsRead = true;
            _messageRepo.Update(message);
        }

        public void DeleteReceiveMessage(int messageId)
        {
            var message = Get1Message(messageId);
            message.IsReceiverDeleted = true;
            _messageRepo.Update(message);
        }

        public void DeleteSendMessage(int messageId)
        {
            var message = Get1Message(messageId);
            message.IsSenderDeleted = true;
            _messageRepo.Update(message);
        }
    }
}
