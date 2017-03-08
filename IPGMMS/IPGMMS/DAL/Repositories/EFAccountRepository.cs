using IPGMMS.Abstract;
using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IPGMMS.DAL.Repositories
{
    public class EFAccountRepository : IAccountRepository
    {
        private ApplicationDbContext adb;

        /// <summary>
        /// Constructor for this repository. Sets the dbcontext for this class with the injected context
        /// </summary>
        /// <param name="appContext">The application dbContext to use for working with the accounts</param>
        public EFAccountRepository(ApplicationDbContext appContext)
        {
            adb = appContext;
        }

        /// <summary>
        /// Gets all of the roles except for the Admin role
        /// </summary>
        public IEnumerable<IdentityRole> GetRoles
        {
            get { return adb.Roles.Where(u => !u.Name.Contains("Admin")).ToList(); }
        }
    }
}