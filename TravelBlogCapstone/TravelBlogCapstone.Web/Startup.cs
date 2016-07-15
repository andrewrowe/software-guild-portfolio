using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TravelBlogCapstone.Web.Models;

[assembly: OwinStartupAttribute(typeof(TravelBlogCapstone.Web.Startup))]
namespace TravelBlogCapstone.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Id = 3.ToString();
                role.Name = "Admin";
                roleManager.Create(role);                  

                var user = new ApplicationUser();
                user.FirstName = "Bruno";
                user.LastName = "Sebag";
                user.Email = "bruno.sebag@gmail.com";
                user.UserName = "bruno.sebag@gmail.com";
                string userPWD = "A@Z200711";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Id = 2.ToString();
                role.Name = "Employee";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Id = 1.ToString();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
