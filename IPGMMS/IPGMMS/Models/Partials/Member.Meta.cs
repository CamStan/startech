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

        [Display(Name = "Website")]
        public string WebAddress { get { return $"http://{Website}"; } }
    }

    public class MemberMetadata
    {
        [Display(Name = "Username")]
        public string UserName;

        [Display(Name = "IPG Member #")]
        public string Membership_Number;

        [Display(Name = "Member Since")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Membership_SignupDate;

        [Display(Name = "Member Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Membership_ExpirationDate;

        [Display(Name = "Member Level")]
        public int MemberLevel;

        [Display(Name = "First Name")]
        public string FirstName;

        [Display(Name = "Middle Name")]
        public string MiddleName;

        [Display(Name = "Last Name")]
        public string LastName;

        [Display(Name = "Business")]
        public string BusinessName;
    }
}