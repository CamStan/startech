using IPGMMS.Abstract;
using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.DAL.Repositories
{
    public class EFPortalRepository : IPortalRepository
    {
        private IPGMMS_Context db;
        private ApplicationDbContext adb;

        /// <summary>
        /// Constructor for this repository. Sets the dbcontext for this class with the injected context
        /// </summary>
        /// <param name="context">The IPG dbContext to use for working with the admin portal</param>
        /// <param name="appContext">The application dbContext to use for working with the admin portal</param>
        public EFPortalRepository(IPGMMS_Context context, ApplicationDbContext appContext)
        {
            db = context;
            adb = appContext;
        }

    }
}