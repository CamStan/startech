using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.Models
{
    public class MapInfo
    {
        public Location position { get; set; }
        public string businessName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string website { get; set; }
    }
}