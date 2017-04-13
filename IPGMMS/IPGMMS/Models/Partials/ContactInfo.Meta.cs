using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPGMMS.Models
{
    [MetadataType(typeof(ContactInfoMetadata))]
    public partial class ContactInfo
    {
    }

    public class ContactInfoMetadata
    {
        [Display(Name = "Street Address")]
        public string StreetAddress;

        [Display(Name = "State")]
        public string StateName;

        [Display(Name = "Country Code")]
        public string Country;

        [Display(Name = "Postal Code")]
        public string PostalCode;

        [Display(Name = "Phone #")]
        public string PhoneNumber;
    }

    [MetadataType(typeof(MailingInfoMetadata))]
    public partial class MailingInfo : ContactInfo
    {
    }

    public class MailingInfoMetadata
    {
        [Required]
        public string Email;
    }
}