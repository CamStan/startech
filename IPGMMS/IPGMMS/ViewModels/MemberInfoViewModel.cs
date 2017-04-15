﻿using IPGMMS.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IPGMMS.ViewModels
{
    public class MemberInfoViewModel
    {
        public Member MemberInfo { get; set; }
        public ContactInfo MailingInfo { get; set; }
        public ContactInfo ListingInfo { get; set; }
    }
}