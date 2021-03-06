﻿using IPGMMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPGMMS.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace IPGMMS.DAL.Repositories
{
    public class EFContactRepository : IContactRepository
    {
        private IPGMMS_Context db;

        /// <summary>
        /// Constructor for this repository. Sets the dbcontext for this class with the injected context
        /// </summary>
        /// <param name="context">The dbContext to use for working with contactInfos</param>
        public EFContactRepository(IPGMMS_Context context)
        {
            db = context;
        }

        /// <summary>
        /// Creates a new ContactInfo based off the EF ContactInfo model
        /// </summary>
        /// <returns>A ContactInfo object based off the EF ContactInfo model</returns>
        public ContactInfo CreateContact()
        {
            return db.ContactInfoes.Create();
        }

        /// <summary>
        /// Finds a ContactInfo object based on an ID
        /// </summary>
        /// <param name="id">The ID of the ContactInfo object to find</param>
        /// <returns>The ContactInfo object matching the desired input ID</returns>
        public ContactInfo Find(int? id)
        {
            return db.ContactInfoes.Find(id);
        }

        /// <summary>
        /// Find the Listing ContactInfo based on a member ID
        /// </summary>
        /// <param name="id">The ID of the member</param>
        /// <returns>The ContactInfo object which is the listing info for the memberID provided
        /// or an empty contact if none exists in database.</returns>
        public ContactInfo ListingInfoFromMID(int? id)
        {
            // Set a default contact ID, -1 because it's out of range
            int listID = -1;

            // Try to find existing ID, catch if there's an error (like ID doesn't exist)
            try
            {
                listID = db.Contacts.Where(s => s.Member_ID == id).Where(x => x.ContactType_ID == 2).First().ContactInfo_ID;
            }
            catch (Exception e)
            {
                Debug.WriteLine("An error occurred: '{0}'", e);
            }

            ContactInfo contInfo = new ContactInfo();
            //If the contact ID exists, find and return it, else return an empty
            //contact
            if (listID != -1)
            {
                contInfo = Find(listID);
            }
            return contInfo;
        }

        /// <summary>
        /// Find the Mailing ContactInfo based on a member ID
        /// </summary>
        /// <param name="id">The ID of the member</param>
        /// <returns>The ContactInfo object which is the mailing info for the memberID provided</returns>
        public ContactInfo MailingInfoFromMID(int? id)
        {
            var listID = db.Contacts.Where(s => s.Member_ID == id).Where(x => x.ContactType_ID == 1).First().ContactInfo_ID;
            var contInfo = Find(listID);
            return contInfo;
        }

        /// <summary>
        /// Fetch the member ID that corresponds to the contact info.
        /// </summary>
        /// <param name="info">ContactInfo object</param>
        /// <returns>The memberID that matches the contactInfoID in the contact table.</returns>
        public int getMemberID(ContactInfo info)
        {
            var memID = db.Contacts.Where(s => s.ContactInfo_ID == info.ID).First().Member_ID;
            return memID;
        }

        /// <summary>
        /// Inserts the input contactInfo into the database if it's a new object, else updates
        /// the entity already in the database. This method only changes the entity's state,
        /// thus the Save() method must be called to make the changes permanant.
        /// </summary>
        /// <param name="contactInfo">The ContactInfo object to insert or update</param>
        public ContactInfo InsertorUpdate(ContactInfo contactInfo)
        {
            if (contactInfo.ID == default(int)) // new Member
            {
                db.ContactInfoes.Add(contactInfo);
            }
            else // Existing Member
            {
                db.Entry(contactInfo).State = EntityState.Modified;
            }
            Save();
            return contactInfo;
        }

        /// <summary>
        /// Changes the ContactInfo object with the input ID to deleted.
        /// Save() must be called to make this change take effect.
        /// </summary>
        /// <param name="id">The ID of the ContactInfo object to delete</param>
        public void Delete(int id)
        {
            ContactInfo contactInfo = Find(id);
            db.ContactInfoes.Remove(contactInfo);
        }

        /// <summary>
        /// Saves the state of any entities that have changed in the database.
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Creates a Contact bridge entity object with the Mailing Contact_Type ID to link the input ContactInfo
        /// with the input Member
        /// </summary>
        /// <param name="member">The Member object to be linked to a ContactInfo</param>
        /// <param name="contactInfo">The ContactInfo object to be linked to a Member</param>
        /// <returns>True if the ojects were able to be linked. IF false, then one of the input objects may not have
        /// been created correctly and may not have a correct primary key ID</returns>
        public bool LinkMailingContact(Member member, ContactInfo contactInfo)
        {
            if (member.ID == default(int) || contactInfo.ID == default(int))
            {
                return false;
            }
            else
            {
                Contact contact = db.Contacts.Create();
                contact.ContactType_ID = db.ContactTypes.Where(c => c.ContactType1.Equals("Mailing")).FirstOrDefault().ID;
                contact.Member_ID = member.ID;
                contact.ContactInfo_ID = contactInfo.ID;
                db.Contacts.Add(contact);
                Save();
                return true;
            }
        }

        /// <summary>
        /// Creates a Contact bridge entity object with the Listing Contact_Type ID to link the input ContactInfo
        /// with the input Member
        /// </summary>
        /// <param name="member">The Member object to be linked to a ContactInfo</param>
        /// <param name="contactInfo">The ContactInfo object to be linked to a Member</param>
        /// <returns>True if the ojects were able to be linked. IF false, then one of the input objects may not have
        /// been created correctly and may not have a correct primary key ID</returns>
        public bool LinkListingContact(Member member, ContactInfo contactInfo)
        {
            if (member.ID == default(int) || contactInfo.ID == default(int))
            {
                return false;
            }
            else
            {
                Contact contact = db.Contacts.Create();
                contact.ContactType_ID = db.ContactTypes.Where(c => c.ContactType1.Equals("Listing")).FirstOrDefault().ID;
                contact.Member_ID = member.ID;
                contact.ContactInfo_ID = contactInfo.ID;
                db.Contacts.Add(contact);
                Save();
                return true;
            }
        }
    }
}