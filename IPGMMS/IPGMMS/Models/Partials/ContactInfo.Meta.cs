using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IPGMMS.Models
{
    [MetadataType(typeof(ContactInfoMetadata))]
    public partial class ContactInfo
    {
        public string mapURL{ get { return $"https://www.google.com/maps/embed/v1/search?key=AIzaSyB75sLTv_4MR8DnNb8PptRe9Acvh9vNzqI&q={formatAddressString},{formatCityString}+{StateName}+{PostalCode}"; } }
 
        private string formatAddressString { get { return StreetAddress.Replace(' ', '+'); } }
        private string formatCityString { get { return City.Replace(' ', '+'); } }
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
        [RegularExpression(@"^((\+|00)[1-9]{1,3})?(\-| {0,1})?(([\d]{0,3})(\-| {0,1})?([\d]{7,15})){1}$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber;
    }
}