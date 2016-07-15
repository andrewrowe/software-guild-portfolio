using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.Models;
using TravelBlogCapstone.Web.ViewModels;

namespace TravelBlogCapstone.Web.Controllers.ApiControllers
{
    [Authorize(Roles = "Admin,Employee,User")]
    public class UserController : ApiController
    {
        public UserManager<ApplicationUser> UserManager;

        public UserController()
        {
            var context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        //When a user write an e-mail, check if the recipient exists
        public bool Get(string SenderUserName)
        {
            ApplicationUser user = new ApplicationUser();
            if (SenderUserName.Contains("@"))
            {
                user = UserManager.FindByEmail(SenderUserName);
            }
            else
            {
                user = UserManager.FindByName(SenderUserName);
            }
            if (user == null)
                return false;
            return true;
        }

        public List<MessagesViewModel> GetSendMessages()
        {
            ApplicationUser user = new ApplicationUser();

            var result = new List<MessagesViewModel>();
            var userId = User.Identity.GetUserId();
            var manager = new UserManager();
            var messages = manager.GetAllSendMessages(userId);

            foreach (var message in messages)
            {
                user = UserManager.FindById(message.RecipientUserId);
                result.Add(new MessagesViewModel() {Message = message, Email = user.Email, UserName = user.UserName});
            }
            return result;
        }

        public List<MessagesViewModel> GetReceiveMessages()
        {
            ApplicationUser user = new ApplicationUser();

            var result = new List<MessagesViewModel>();
            var userId = User.Identity.GetUserId();
            var manager = new UserManager();
            var messages = manager.GetAllReceiveMessages(userId);

            foreach (var message in messages)
            {
                user = UserManager.FindById(message.SendUserId);
                result.Add(new MessagesViewModel() { Message = message, Email = user.Email, UserName = user.UserName });
            }
            return result;
        }

        [ActionName("DeleteReceiveMessage")]
        [HttpPut]
        public void DeleteReceiveMessage(int id)
        {
            var manager = new UserManager();
            manager.DeleteReceiveMessage(id);
        }

        [ActionName("DeleteSendMessage")]
        [HttpPut]
        public void DeleteSendMessage(int id)
        {
            var manager = new UserManager();
            manager.DeleteSendMessage(id);
        }
    }
}
