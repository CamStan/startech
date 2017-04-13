
using IPGMMS.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IPGMMS.ViewModels
{
    public class MemberCreate
    {
        public Member MemberInfo { get; set; }
        public MailingInfo MailingInfo { get; set; }
        public ContactInfo ListingInfo { get; set; }
        public IEnumerable<SelectListItem> Levels { get; set; }

    }
}