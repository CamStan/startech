using IPGMMS.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IPGMMS.ViewModels
{
    public class MemberInfoVM
    {
        public Member MemberInfo { get; set; }
        public IEnumerable<SelectListItem> Levels { get; set; }
    }
}