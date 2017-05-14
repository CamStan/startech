using IPGMMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.Models
{
    public class MapInfo
    {
        public Location Position { get; set; }
        public string FullName { get; set; }
        public string BusinessName { get; set; }
        public string MemberLevel { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}