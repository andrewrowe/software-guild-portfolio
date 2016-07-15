using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.Models;
using TravelBlogCapstone.Web.ViewModels;

namespace TravelBlogCapstone.Web.Controllers
{
    [Authorize(Roles = "Admin,Employee,User")]
    public class UserController : ApplicationBaseController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<ActionResult> Index(UserController.ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == UserController.ManageMessageId.ChangePasswordSuccess ? "Password has been changed."
                : message == UserController.ManageMessageId.SetPasswordSuccess ? "Password has been set."
                : message == UserController.ManageMessageId.ChangeFirstNameSuccess ? "First name has been changed."
                : message == UserController.ManageMessageId.ChangeLastNameSuccess ? "Last name has been changed."
                : message == UserController.ManageMessageId.ChangeAgeSuccess ? "Age has been changed."
                : message == UserController.ManageMessageId.ChangeBioSuccess ? "Bio has been changed."
                : message == UserController.ManageMessageId.SendMessageSuccess ? "Message has been sent."
                : message == UserController.ManageMessageId.Error ? "An error has occurred."
                : "";
            var repo = new UserManager();

            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            var model = new UserProfile();
            if (!string.IsNullOrEmpty(username))
            {
                model.ApplicationUser = context.Users.SingleOrDefault(u => u.UserName == username);
                model.NumberNoReadMessages = repo.CountNoReadMessages(User.Identity.GetUserId());
                if (model.ApplicationUser.Bio != null)
                    model.ApplicationUser.Bio = model.ApplicationUser.Bio.Replace("\r\n", "<br/>");
                else
                    model.ApplicationUser.Bio = "Not defined";
            }
            return View(model);
        }

        public ActionResult DisplayAnotherProfile(string id)
        {
            //If Actual User
            if (User.Identity.Name == id)
                return RedirectToAction("Index");
            ApplicationUser user = UserManager.FindByName(id);
            user.Bio = user.Bio.Replace("\r\n", "<br/>");
            return View(user);
        }

        public ActionResult DisplayAMessage(int id)
        {
            var manager = new UserManager();
            var message = manager.Get1Message(id);
            message.SenderUserName = UserManager.FindById(message.SendUserId).UserName;
            if (HttpContext.Request.UrlReferrer.ToString().Contains("Receive"))
            {
                ViewBag.Previous = "Receive";
                if (!message.IsRead)
                    manager.PutMessageAsRead(message);
            }
            else
                ViewBag.Previous = "Send";

            message.Body = message.Body.Replace("\r\n", "<br/>");
            return View(message);
        }

        public ActionResult SendMessages(string id)
        {
            var userId = UserManager.FindByName(id).Id;
            var manager = new UserManager();
            var model = manager.GetAllSendMessages(userId);
            return View(model);
        }

        public ActionResult ReceiveMessages(string id)
        {
            var userId = UserManager.FindByName(id).Id;
            var manager = new UserManager();
            var model = manager.GetAllReceiveMessages(userId);
            return View(model);
        }

        public ActionResult AnswerAMessage(int id)
        {
            Message model = new Message();
            var manager = new UserManager();
            var message = manager.Get1Message(id);
            model.SenderUserName = UserManager.FindById(message.SendUserId).UserName;
            if (message.Subject != "")
                model.Subject = "Re: " + message.Subject;
            return View("SendAMessage", model);
        }

        public ActionResult SendAMessage(string id = "")
        {
            Message model = new Message();
            if (id != "")
            {
                model.SenderUserName = id;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAMessage(Message message)
        {
            ApplicationUser user = new ApplicationUser();
            if (message.SenderUserName.Contains("@"))
                user = UserManager.FindByEmail(message.SenderUserName);
            else
                user = UserManager.FindByName(message.SenderUserName);

            message.RecipientUserId = user.Id;
            message.SendUserId = User.Identity.GetUserId();
            message.SendDate = DateTime.Now;
            
            var repo = new UserManager();
            repo.CreateMessage(message);

            return RedirectToAction("Index", new { Message = ManageMessageId.SendMessageSuccess });
        }

        public ActionResult ChangeFirstName(string id)
        {
            var model = new ApplicationUser() { FirstName = id };
            return View(model);
        }

        public ActionResult ChangeLastName(string id)
        {
            var model = new ApplicationUser() { LastName = id };
            return View(model);
        }

        public ActionResult ChangeAge(string id)
        {
            var age = (id == null) ? 0 : int.Parse(id);
            var model = new ApplicationUser() { Age = age };
            return View(model);
        }

        public ActionResult ChangeBio()
        {
            ApplicationUser user = new ApplicationUser();
            user = UserManager.FindById(User.Identity.GetUserId());
            var model = new ApplicationUser() { Bio = user.Bio };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeFirstName(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationUser);
            }
            var oldapplicationUser = UserManager.FindById(User.Identity.GetUserId());
            oldapplicationUser.FirstName = applicationUser.FirstName;

            UserManager.Update(oldapplicationUser);

            return RedirectToAction("Index", new {Message = ManageMessageId.ChangeFirstNameSuccess});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLastName(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationUser);
            }
            var oldapplicationUser = UserManager.FindById(User.Identity.GetUserId());
            oldapplicationUser.LastName = applicationUser.LastName;

            UserManager.Update(oldapplicationUser);

            return RedirectToAction("Index", new { Message = ManageMessageId.ChangeLastNameSuccess });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAge(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationUser);
            }
            var oldapplicationUser = UserManager.FindById(User.Identity.GetUserId());
            oldapplicationUser.Age = applicationUser.Age;

            UserManager.Update(oldapplicationUser);

            return RedirectToAction("Index", new { Message = ManageMessageId.ChangeAgeSuccess });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeBio(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationUser);
            }
            var oldapplicationUser = UserManager.FindById(User.Identity.GetUserId());
            oldapplicationUser.Bio = applicationUser.Bio;

            UserManager.Update(oldapplicationUser);

            return RedirectToAction("Index", new { Message = ManageMessageId.ChangeBioSuccess });
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            ChangeFirstNameSuccess,
            ChangeLastNameSuccess,
            ChangeAgeSuccess,
            ChangeBioSuccess,
            SendMessageSuccess,
            Error
        }
    }
}