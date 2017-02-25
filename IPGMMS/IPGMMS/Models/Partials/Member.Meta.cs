using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPGMMS.Models
{
    [MetadataType(typeof(MemberMetadata))]
    public partial class Member
    {
        [Display(Name = "Member")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }

    public class MemberMetadata
    {
        [Display(Name = "Business")]
        public string BusinessName;
    }
}