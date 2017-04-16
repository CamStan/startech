using IPGMMS.Abstract;
using IPGMMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Diagnostics;
using IPGMMS.ViewModels;

namespace IPGMMS.Controllers
{
    [AdminAuthorize(Roles = "Admin")]
    public class PortalController : MController
    {
        private IMemberRepository memberRepo;
        private IContactRepository contactRepo;

        public PortalController(IMemberRepository mRepo, IContactRepository cRepo)
        {
            memberRepo = mRepo;
            contactRepo = cRepo;
        }

        //***********************************************ADMIN PORTAL INDEX*****************************
        // GET: Portal
        public ActionResult Index()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;

            return View();
        }

        //***********************************************LIST MEMBER INFO*****************************
        public ActionResult ListMembers(int? page, string sortOrder, string searchString)
        {

            var members = memberRepo.GetAllMembers;

            //This section is to check the search string and return a member list that
            //has only the information that was searched for 
            //Credit for filtering and some paging code goes to: 
            //docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.LastName.ToLower().Contains(searchString.ToLower())
                                       || s.FirstName.ToLower().Contains(searchString.ToLower()));
            }

            // It will switch the stored variable so it will use the other
            // next time.
            ViewBag.lName = String.IsNullOrEmpty(sortOrder) ? "l_name" : "";
            ViewBag.mem_num = sortOrder == "mem_num" ? "num_desc" : "mem_num";
            ViewBag.date_start = sortOrder == "date_start" ? "start_desc" : "date_start";
            ViewBag.date_end = sortOrder == "date_end" ? "end_desc" : "date_end";
            ViewBag.f_name = sortOrder == "f_name" ? "fname_desc" : "f_name";
            ViewBag.b_name = sortOrder == "b_name" ? "bname_desc" : "b_name";
            ViewBag.mem_lvl = sortOrder == "mem_lvl" ? "lvl_desc" : "mem_lvl";

            // Initialize the variable that will become the sort option
            Func<Member, object> sorting;

            // Check each of the four dictionaries for sort params
            // Dictionary is at the bottom of file
            if (!String.IsNullOrEmpty(sortOrder))
            {
                if (sortBy.TryGetValue(sortOrder, out sorting))
                {
                    members = members.OrderBy(sorting);
                }
                else if (sortByDesc.TryGetValue(sortOrder, out sorting))
                {
                    members = members.OrderByDescending(sorting);
                }
                else // Shouldn't need this but will catch any non-match
                {
                    members = members.OrderByDescending(m => m.LastName);
                }
            }
            else // Catch if sortOrder is null or empty
            {
                members = members.OrderByDescending(m => m.LastName);
            }

            int pageSize = 5; //the number of items that can appear on each page.
            int startPage = (page ?? 1);

            return View("ListMembers", members.ToList().ToPagedList(startPage, pageSize));
        }
        //***********************************************DETAILED MEMBER INFO*****************************
        public ActionResult DetailMember()
        {
            return View("DetailMember");
        }
        //***********************************************ADD MEMBER*****************************
        // GET: Addmember()
        public ActionResult AddMember()
        {
            MemberCreate createMember = new MemberCreate();
            createMember.Levels = memberRepo.GetLevels;
            return View("AddMember", createMember);
        }

        // POST: AddMember()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(MemberCreate infos)
        {

            if (ModelState.IsValid)
            {
                Member memb = infos.MemberInfo;
                memb = memberRepo.InsertorUpdate(memb);
                // Mailing info is required.
                ContactInfo mail = (ContactInfo)infos.MailingInfo;
                mail = contactRepo.InsertorUpdate(mail);
                contactRepo.LinkMailingContact(memb, mail);

                // Listing info is optional.
                ContactInfo list = infos.ListingInfo;
                list = contactRepo.InsertorUpdate(list);
                contactRepo.LinkListingContact(memb, list);

                Debug.WriteLine("Says it's valid but not really, maybe");
                return View("DetailMember", memb);
            }
            infos.Levels = memberRepo.GetLevels;
            return View("AddMember", infos);
        }

        public ActionResult UpdateMember(int? id)
        {
            if (id == null)
            {
                id = 4;
            }
            MemberInfoViewModel info = new MemberInfoViewModel();
            info.MemberInfo = memberRepo.Find(id);
            info.ListingInfo = contactRepo.ListingInfoFromMID(id);
            info.MailingInfo = contactRepo.MailingInfoFromMID(id);
            return View(info);
        }

        //***********************************************UPDATE MEMBER INFO*****************************
        /// <summary>
        /// This method is the GET for UpdateMemberInfo(). This takes in the memberID 
        /// (not to be confused with the memberNumber) and, if a valid memberID, returns
        /// a form that allows the user to update member information.
        /// </summary>
        /// <param name="memID"></param>
        /// <returns>The UpdateMemberInfo view. </returns>
        public ActionResult UpdateMemberInfo(int? memID)
        {
            if (memID == null)
            {
                return View(Request.UrlReferrer.ToString());
            }
            if (memberRepo.Find(memID) == null)
            {
                return View(Request.UrlReferrer.ToString());
            }

            MemberInfoVM memberinfo = new MemberInfoVM();
            memberinfo.Levels = memberRepo.GetLevels;
            memberinfo.MemberInfo = memberRepo.Find(memID);

            return View(memberinfo);
        }

        /// <summary>
        /// This method is the POST for UpdateMemberInfo(). This takes in a ViewModel parameter
        /// that holds a member object and a select list of membership levels. This method will
        /// save updates to the database and return the user to the UpdateMember page to view 
        /// their changes.
        /// </summary>
        /// <param name="memb"></param>
        /// <returns>If the model state is valid, this returns the user back to the UpdateMember view
        /// if it is not valid, it returns the user to the UpdateMemberInfo view with the ViewModel given.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemberInfo(MemberInfoVM memb)
        {
            if (ModelState.IsValid)
            {
                var member = memb.MemberInfo;
                memberRepo.InsertorUpdate(member);
                MemberInfoViewModel memberDetails = new MemberInfoViewModel();
                memberDetails.MemberInfo = member;
                memberDetails.ListingInfo = contactRepo.ListingInfoFromMID(member.ID);
                memberDetails.MailingInfo = contactRepo.MailingInfoFromMID(member.ID);
                memb.Levels = memberRepo.GetLevels;
                return View("UpdateMember", memberDetails);
            }

            memb.Levels = memberRepo.GetLevels;
            return View(memb);
        }

        // GET: UpdateMemberMailing()
        public ActionResult UpdateMemberMailing(int? memID)
        {
            if (memID == null)
            {
                return View(Request.UrlReferrer.ToString());
            }

            return View(contactRepo.MailingInfoFromMID(memID));
        }

        // POST: UpdateMemberMailing()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemberMailing(ContactInfo info)
        {
            contactRepo.InsertorUpdate(info);
            return View();
        }

        // GET: UpdateMemberListing()
        public ActionResult UpdateMemberListing(int? memID)
        {
            if (memID == null)
            {
                return View(Request.UrlReferrer.ToString());
            }

            return View(contactRepo.ListingInfoFromMID(memID));
        }

        // POST: UpdateMemberListing()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemberListing(ContactInfo info)
        {
            contactRepo.InsertorUpdate(info);
            return View();
        }

        public ActionResult ListTests()
        {
            return View("ListTests");
        }

        public ActionResult AddTest()
        {
            return View("AddTest");
        }

        public ActionResult ListCertifications()
        {
            return View("ListCertifications");
        }

        public ActionResult DetailCertification()
        {
            return View("DetailCertification");
        }

        public ActionResult AddCertification()
        {
            return View("AddCertification");
        }

        public ActionResult UpdateCertification()
        {
            return View("UpdateCertification");
        }

        // Two dictionaries, one for ascending, one for descending
        Dictionary<String, Func<Member, object>> sortBy = new Dictionary<String, Func<Member, object>>()
            {
                { "mem_num", m => m.Membership_Number },
                { "date_start", m => m.Membership_SignupDate },
                { "date_end", m => m.Membership_ExpirationDate },
                { "f_name", m => m.FirstName },
                { "l_name", m => m.LastName },
                { "b_name", m => m.BusinessName },
                { "mem_lvl", m => m.MemberLevel1.MLevel }
            };

        Dictionary<String, Func<Member, object>> sortByDesc = new Dictionary<String, Func<Member, object>>()
            {
                { "num_desc", m => m.Membership_Number },
                { "start_desc", m => m.Membership_SignupDate },
                { "end_desc", m => m.Membership_ExpirationDate },
                { "fname_desc", m => m.FirstName },
                { "lname_desc", m => m.LastName },
                { "bname_desc", m => m.BusinessName },
                { "lvl_desc", m => m.MemberLevel1.MLevel }
            };
    }
}