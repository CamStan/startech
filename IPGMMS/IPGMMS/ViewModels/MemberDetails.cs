// This class is for public display of member details. Since not all 
// information is to be made public, this object will only contain the 
// neccessary info that this presented to the public. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.ViewModels
{
    public class MemberDetails
    {
        public string FullName { get; set; }
        public string BusinessName { get; set; }
        public string MemberLevel { get; set; }
        public int MemberLevelbyInt { get; set; }
        public string LevelAbbrev { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public Models.ContactInfo Contact { get; set; }

        // There can be more added for the form questions
        // ie. self-wash available, mobile grooming, etc.
        // possibly an array of values
    }
}