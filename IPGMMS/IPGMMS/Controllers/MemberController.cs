using IPGMMS.Abstract;
using IPGMMS.Models;
using IPGMMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IPGMMS.Controllers
{
    public class MemberController : MController
    {
        private IMemberRepository memberRepo;
        private IContactRepository contactRepo;

        public MemberController(IMemberRepository repo, IContactRepository cRepo)
        {
            memberRepo = repo;
            contactRepo = cRepo;
        }

        /// <summary>
        /// GET: Members
        /// Displays all members sorted by last name, 3 at a time on a page with paging.
        /// </summary>
        /// <param name="page">The page number</param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            var members = memberRepo.GetAllMembers;

            int pageSize = 3;
            double pages = Math.Ceiling((double)members.Count() / pageSize);

            int pageNum = page ?? 1;

            ViewBag.Pages = pages;

            var membersPaged = members.Skip(pageSize * (pageNum - 1)).Take(pageSize);

            return View(membersPaged);
        }

        // GET: Member
        public ActionResult Details(int? ID)
        {
            if (!ID.HasValue)
            {
                ID = 5;
            }
            Member memb = memberRepo.Find(ID);
            return View(memb);
        }

        /// <summary>
        /// GET: Member Create
        /// This is the GET for the application form for a user to provide thier basic information in order to apply to become an IPG member.
        /// This from requires the user to have already registered on the website.
        /// </summary>
        /// <returns>A View object for the Member/Apply page or a redirect to their details page if already a member</returns>
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Apply()
        {
            var userID = User.Identity.GetUserId();
            if(memberRepo.FindByIdentityID(userID) == null) // current Identity user doesn't already have an associated IPG account/application
            {
                MemberInfoViewModel newMember = new MemberInfoViewModel();
                newMember.MemberInfo = memberRepo.CreateMember();
                //autopopulate the user's email, member level, and identity ID
                newMember.MailingInfo = contactRepo.CreateContact();
                newMember.MailingInfo.Email = UserManager.GetEmail(userID);
                newMember.MemberInfo.MemberLevel = memberRepo.GetMemberLevelID("Uncategorized");
                newMember.MemberInfo.Identity_ID = userID;
                return View(newMember);
            }
            else // current Identity user has already applied or is already an IPG member
            {
                return RedirectToAction("Index", "Manage");
            }
        }

        /// <summary>
        /// POST: Member Create
        /// The post method for the application form for a user that is applying to become and IPG member
        /// </summary>
        /// <param name="newMember">The viewmodel containing the information provided by the applying user</param>
        /// <returns>A redirect view object to the user's details page if successful, otherwise a view object
        /// back to the application form</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(MemberInfoViewModel newMember)
        {
            if (ModelState.IsValid)
            {
                Member memb = newMember.MemberInfo;
                ContactInfo mail = newMember.MailingInfo;
                ContactInfo list = newMember.ListingInfo;
                // set signup date to today and generate member number
                memb.Membership_SignupDate = DateTime.Today;
                memb.Membership_Number = memberRepo.setMemberNumber(memb, mail);
                // insert member and mailingInfo and link the two
                memb = memberRepo.InsertorUpdate(memb);
                mail = contactRepo.InsertorUpdate(mail);
                contactRepo.LinkMailingContact(memb, mail);
                // insert listingInfo and link to member
                list = contactRepo.InsertorUpdate(list);
                contactRepo.LinkListingContact(memb, list);

                return RedirectToAction("Index", "Manage");
            }

            return View(newMember);
        }
    }
}