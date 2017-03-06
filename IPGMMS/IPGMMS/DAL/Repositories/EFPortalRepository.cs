using IPGMMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.DAL.Repositories
{
    public class EFPortalRepository : IPortalRepository
    {
        private IPGMMS_Context db;

        /// <summary>
        /// Constructor for this repository. Sets the dbcontext for this class with the injected context
        /// </summary>
        /// <param name="context">The dbContext to use for working with members</param>
        public EFPortalRepository(IPGMMS_Context context)
        {
            db = context;
        }

    }
}