using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.IRepository
{
    public interface IMessagesRepository
    {
        List<Message> GetAllForUser(string userId);
        Message Get(int messageId);
        Message Insert(Message message);
        void Delete(int messageId);
        void Update(Message message);
    }
}
