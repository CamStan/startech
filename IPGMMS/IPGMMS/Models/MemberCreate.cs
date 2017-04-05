using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.Models
{
    public class MemberCreate
    {
        public Member MemberInfo { get; set; }
        public ContactInfo MailingInfo { get; set; }
        public ContactInfo ListingInfo { get; set; }
        public MemberLevel Levels { get; set; }

    }
}