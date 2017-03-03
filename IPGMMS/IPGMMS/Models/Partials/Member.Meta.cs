using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IPGMMS.Models;

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

        [Display(Name = "Member Since")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Membership_SignupDate;

        [Display(Name = "Member Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Membership_ExpirationDate;
    }
}