using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.Models;
using TravelBlogCapstone.Web.ViewModels;

namespace TravelBlogCapstone.Web.Controllers.ApiControllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : ApiController
    {
        public UserManager<ApplicationUser> UserManager;

        public RoleController()
        {
            var context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        [ActionName("GetRoles")]
        public List<UsersRoles> GetRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roles = roleManager.Roles;
            var usersRoles = UserManager.Users.Select(e => new UsersRoles() { UserId = e.Id, FirstName = e.FirstName, LastName = e.LastName, RoleId = e.Roles.FirstOrDefault().RoleId }).ToList();
            
            var query = from user in usersRoles
                        join role in roles on user.RoleId equals role.Id
                        select
                            new UsersRoles()
                            {
                                RoleId = user.RoleId,
                                RoleName = role.Name,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserId = user.UserId
                            };
            return query.OrderBy(u => u.RoleId).ThenBy(v => v.LastName).ToList();
        }

        [ActionName("UpgradeUserToEmployee")]
        [HttpPut]
        public void UpgradeUserToEmployee(string id)
        {
            UserManager.RemoveFromRole(id, "User");
            UserManager.AddToRole(id, "Employee");
        }

        [ActionName("UpgradeEmployeeToAdmin")]
        [HttpPut]
        public void UpgradeEmployeeToAdmin(string id)
        {
            UserManager.RemoveFromRole(id, "Employee");
            UserManager.AddToRole(id, "Admin");
        }

        [ActionName("DowngradeEmployeeToUser")]
        [HttpPut]
        public void DowngradeEmployeeToUser(string id)
        {
            UserManager.RemoveFromRole(id, "Employee");
            UserManager.AddToRole(id, "User");
        }

        [ActionName("DowngradeAdminToEmployee")]
        [HttpPut]
        public void DowngradeAdminToEmployee(string id)
        {
            UserManager.RemoveFromRole(id, "Admin");
            UserManager.AddToRole(id, "Employee");
        }

        [ActionName("DeleteUser")]
        [HttpPut]
        public void DeleteUser(string id)
        {
            UserManager.Delete(UserManager.FindById(id));
        }
    }
}
