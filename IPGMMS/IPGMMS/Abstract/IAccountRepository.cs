using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IPGMMS.Abstract
{
    public interface IAccountRepository
    {
        IEnumerable<IdentityRole> GetRoles { get; }
    }
}