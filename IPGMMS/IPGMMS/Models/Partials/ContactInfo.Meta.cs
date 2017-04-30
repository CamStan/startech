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
        /// <summary>
        /// This method returns the GoogleMap url based on the address given in the Contact Info. If the StreetAddress
        /// and the City and the StateName are all null, then the url returned will render only a map of the world. Otherwise
        /// it will send any location information available. There are 2 private methods below the mapURL method that format
        /// the address and the city to replace any white space with a +, which is how the string needs to be formatted 
        /// to render the map.
        /// </summary>
        public string mapURL
        {
            get
            {
                if ((StreetAddress == null) && (City == null) && (StateName == null))
                {
                    return $"https://www.google.com/maps/embed/v1/search?key=AIzaSyB75sLTv_4MR8DnNb8PptRe9Acvh9vNzqI&q=,";
                }
                else
                {
                    return $"https://www.google.com/maps/embed/v1/search?key=AIzaSyB75sLTv_4MR8DnNb8PptRe9Acvh9vNzqI&q={formatAddressString},{formatCityString}+{StateName}+{PostalCode}";
                }
            }
        }

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