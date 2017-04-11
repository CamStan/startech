using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.Abstract
{
    public interface IContactRepository
    {
        ContactInfo CreateContact();
        ContactInfo Find(int? id);
        ContactInfo ListingInfoFromMID(int? id)
        ContactInfo MailingInfoFromMID(int? id)
        void InsertorUpdate(ContactInfo contactInfo);
        void Delete(int id);
        void Save();

        bool LinkMailingContact(Member member, ContactInfo contactInfo);

        bool LinkListingContact(Member member, ContactInfo contactInfo);
    }
}