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
                user.Email = "flightofthe@valhalla.com";

                string userPWD = "W@gner3";

                var chkUser = UserManager.Create(user, userPWD);

                // Add default User to Role Admin
                if(chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Student role
            if (!roleManager.RoleExists("Student_Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student_Member";
                roleManager.Create(role);
            }

            // creating base member role
            if(!roleManager.RoleExists("IPG_Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "IPG_Member";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "DeadPool";
                user.Email = "shoop@badoop.com";

                string userPWD = "D3adp00l";

                var chkUser = UserManager.Create(user, userPWD);

                // Add default User to Role IPG_Member
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "IPG_Member");
                }
            }

            // creating Certified Professional Groomer
            if (!roleManager.RoleExists("Certified_Professional_Groomer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Certified_Professional_Groomer";
                roleManager.Create(role);
            }

            // creating Certified Advanced Professional Groomer
            if (!roleManager.RoleExists("Certified_Advanced_Professional_Groomer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Certified_Advanced_Professional_Groomer";
                roleManager.Create(role);
            }

            // creating International Certified Master Groomer
            if (!roleManager.RoleExists("International_Certified_Master_Groomer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "International_Certified_Master_Groomer";
                roleManager.Create(role);
            }

            // creating Approved Salon
            if (!roleManager.RoleExists("Approved_Salon"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Approved_Salon";
                roleManager.Create(role);
            }

             // creating Approved School
            if (!roleManager.RoleExists("Approved_School"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Approved_School";
                roleManager.Create(role);
            }

            // creating Member School
            if (!roleManager.RoleExists("Member_School"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Member_School";
                roleManager.Create(role);
            }

            // creating Uncategorized
            if (!roleManager.RoleExists("Uncategorized"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Uncategorized";
                roleManager.Create(role);
            }

        }
    }
}
