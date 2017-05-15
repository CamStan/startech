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
        /// Gets a list of all new members sorted by date added.
        /// </summary>
        public IEnumerable<Member> NewMembers
        {
            get { return db.Members.Where(m => m.MemberLevel==9).OrderBy(m => m.Membership_SignupDate).ToList(); }
        }

        /// <summary>
        /// Gets a list of all members who will expire in the next two months sorted by date expiring.
        /// </summary>
        public IEnumerable<Member> ExpiringMembers
        {
            get {
                DateTime expire = DateTime.Now.AddMonths(2);
                DateTime now = DateTime.Now;
                return db.Members.Where(m => m.Membership_ExpirationDate<expire).Where(n => n.Membership_ExpirationDate>now).OrderBy(m => m.Membership_ExpirationDate).ToList(); }
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
        /// Finds the member by their Identity ID string
        /// </summary>
        /// <param name="id">The Identity ID of the member to find</param>
        /// <returns>A Member Object</returns>
        public Member FindByIdentityID(string id)
        {
            return (Member)db.Members.Where(m => m.Identity_ID == id).FirstOrDefault();
        }

        /// <summary>
        /// Finds the member with the correspsonding IPG Member ID
        /// </summary>
        /// <param name="id">The IPG Member ID of the member to find</param>
        /// <returns>The Member Object with the input member ID, or null if none</returns>
        public Member FindByIPG_ID(string id)
        {
            return db.Members.Where(i => i.Membership_Number == id).FirstOrDefault();
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
            //var place = db.ContactInfoes.Where(s => s.Member_ID == id).FirstOrDefault().Country;
            int cTypeID = db.ContactTypes.Where(ct => ct.ContactType1.Equals("Mailing")).FirstOrDefault().ID;
            var place = db.Contacts.Where(c => c.Member_ID == id && c.ContactType_ID == cTypeID).FirstOrDefault().ContactInfo.Country;
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
            string memberNum = "";
            
            if (success)
            {
                i = i + 1; // increment to next number
                i = i * 10; // Add a zero to the end to place the 'random' number
                i += rnum; // Replace the zero at the end with a random int 0-9

                string newNum = string.Format("{00000}", i);
                memberNum = country + newNum;
                memb.Membership_Number = memberNum;
                db.Entry(memb).State = EntityState.Modified;
                Save();
                
            }
            return memberNum;
        }

        /// <summary>
        /// Generates a new member number based on the last member number from
        /// that country. Adds a random digit at the end to prevent two members
        /// that sign up at the same time from possibly having consecutive
        /// numbers.
        /// </summary>
        /// <param name="memb">Member Object</param>
        /// <param name="info">ContactInfo Object</param>
        /// <returns></returns>
        public string setMemberNumber(Member memb, ContactInfo info)
        {
            if (memb == null)
            {
                return "0";
            }
            string country = info.Country;
            if(country == null)
            {
                return "0000000";
            }
            else
            {
                if (country == "USA")
                {
                    country = "01";
                }
                else if (country == "Canada")
                {
                    country = "02";
                }
                else if (country == "UK")
                {
                    country = "03";
                }
                else
                {
                    country = "00";
                }
            }

            Random rand = new Random();
            int rnum = rand.Next(9);

            string lastMem = db.Members.Where(s => s.Membership_Number.StartsWith(country)).OrderByDescending(x => x.Membership_Number).FirstOrDefault().Membership_Number;
            if (lastMem == null)
            {
                lastMem = (country + "2000" + "0");
            }

            string num = lastMem.Substring(2, 5);
            int i = 0;
            bool success = Int32.TryParse(num, out i);
            string memberNum = "";

            if (success)
            {
                i = i * 10;
                i += rnum;

                string newNum = string.Format("{00000}", i);
                memberNum = country + i;
                memb.Membership_Number = memberNum;
                return memberNum;

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

        /// <summary>
        /// Gets the ID for the input member level
        /// </summary>
        /// <param name="level">The member level to look for as a string</param>
        /// <returns>The ID of the input member level</returns>
        public int GetMemberLevelID(string level)
        {
            return db.MemberLevels.Where(ml => ml.MLevel.Equals(level)).FirstOrDefault().ID;
        }
        
        public int[] GetActiveMemberIDs()
        {
            var active_Members = db.Members.Where(mDate => mDate.Membership_ExpirationDate >= DateTime.Today);
            List<int> memberIDs = new List<int>();

            foreach(Member mem in active_Members)
            {
                memberIDs.Add(mem.ID);
            }
            int[] memIDs = memberIDs.ToArray();
            return memIDs;
        }
    }
}