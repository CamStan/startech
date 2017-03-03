using IPGMMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IPGMMS.Startup))]
namespace IPGMMS
{
    public partial class Startup
    {
        // copied from code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup I am creating first Admin Role and creating a default Admin User
            if(!roleManager.RoleExists("Admin"))
            {
                // first we create Admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website

                var user = new ApplicationUser();
                user.UserName = "Valkyrie";
                user.Email = "honor@valhalla.com";

                string userPWD = "Odinisl1fe!";

                var chkUser = UserManager.Create(user, userPWD);

                // Add default User to Role Admin
                if(chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            // creating Creating Employee role
            if(!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "DeadPool";
                user.Email = "shoop@badoop.com";

                string userPWD = "D3adp00l";

                var chkUser = UserManager.Create(user, userPWD);

                // Add default User to Role Employee
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Employee");
                }
            }
        }
    }
}
