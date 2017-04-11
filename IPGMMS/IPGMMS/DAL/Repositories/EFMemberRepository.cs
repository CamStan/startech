using IPGMMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPGMMS.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace IPGMMS.DAL.Repositories
{
    public class EFMemberRepository : IMemberRepository
    {
        private IPGMMS_Context db;

        /// <summary>
        /// Constructor for this repository. Sets the dbcontext for this class with the injected context
        /// </summary>
        /// <param name="context">The dbContext to use for working with members</param>
        public EFMemberRepository(IPGMMS_Context context)
        {
            db = context;
        }

        /// <summary>
        /// Gets a list of all members except for the admin member sorted by last name.
        /// </summary>
        public IEnumerable<Member> GetAllMembers
        {
            get { return db.Members.Where(m => m.ID > 1).OrderBy(m => m.LastName).ToList(); }
        }

        /// <summary>
        /// Creates a new member based on the EF Member model
        /// </summary>
        /// <returns>A new member based on the EF Member model</returns>
        public Member CreateMember()
        {
            return db.Members.Create();
        }

        /// <summary>
        /// Finds the member entity with the input ID
        /// </summary>
        /// <param name="id">The ID of the member to find.</param>
        /// <returns></returns>
        public Member Find(int? id)
        {
            return db.Members.Find(id);
        }

        

        /// <summary>
        /// Inserts the input member into the database if it's a new member, else updates
        /// the member entity already in the database. This method only changes the entity's state,
        /// thus the Save() method must be called to make the changes permanant.
        /// </summary>
        /// <param name="member">The Member to insert or update</param>
        public Member InsertorUpdate(Member member)
        {
            if (member.ID == default(int)) // new Member
            {
                db.Members.Add(member);
            }
            else // Existing Member
            {
                db.Entry(member).State = EntityState.Modified;
            }
            Save();
            return member;
        }

        /// <summary>
        /// Changes the entity state of the Member with the input ID to being deleted.
        /// Save() must be called to make this change take effect.
        /// </summary>
        /// <param name="id">The ID of the Member to be deleted</param>
        public void Delete(int id)
        {
            Member member = Find(id);
            db.Members.Remove(member);
        }

        /// <summary>
        /// Saves the state of any entities that have changed in the database.
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        /// <summary>
        /// Takes in a member ID and returns the numeric code for the country in their contact table.
        /// </summary>
        /// <param name="id">The member primary key</param>
        /// <returns>returns country id as a string</returns>
        public string getCountry(int id)
        {
            //Fetch the country from the db
            //assign country a number, potentially have a list using 2 digits
            //make us a 1 and uk a 2 and aus a 3 etc etc....
            var place = db.ContactInfoes.Where(s => s.Member_ID == id).FirstOrDefault().Country;
            if (place == null)
            {
                return "00";
            }
            if (place == "USA")
            {
                return "01";
            }
            if (place == "Canada")
            {
                return "02";
            }
            if (place == "UK")
            {
                return "03";
            }

            return "00";
        }

        /// <summary>
        /// Given a member, a new member number will be assigned and returned.
        /// if 0000000 is returned the country code could not be created.
        /// </summary>
        /// <param name="memb">Object of type member</param>
        /// <returns></returns>
        public string setMemberNumber(Member memb)
        {
            if (memb == null)
            {
                return "0";
            }
            string country = getCountry(memb.ID);
            if (country == "00")
            {
                return "0000000";
            }
            Random rand = new Random();
            int rnum = rand.Next(9);

            string lastMem = db.Members.Where(s => s.Membership_Number.StartsWith(country)).OrderByDescending(x => x.Membership_Number).FirstOrDefault().Membership_Number;
            if (lastMem == null)
            {
                lastMem = (country+"2000"+"0");
            }

            string num = lastMem.Substring(2, 5);
            int i = 0;
            bool success = Int32.TryParse(num, out i);
            
            
            if (success)
            {
                i = i * 10;
                i += rnum;

                string newNum = string.Format("{00000}", i);
                string memberNum = country + i;
                memb.Membership_Number = memberNum;
                
                // save the member number to the member's entry in the db.
                // Implement this later so we can check for bugs before then!
            }
            return memberNum;
        }
        // Add other functionalities pertaining to Memebers here




        public IEnumerable<SelectListItem> GetLevels
        {
            get
            {
                var _levels = db.MemberLevels.Select(l => new SelectListItem
                {
                    Value = l.ID.ToString(),
                    Text = l.MLevel
                });

                return DefaultLevel.Concat(_levels);
            }
        }
        public IEnumerable<SelectListItem> DefaultLevel
        {
            get {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "Select a level"
                }, count: 1);
            }
        }
    }
}