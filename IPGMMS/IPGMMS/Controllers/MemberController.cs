﻿using IPGMMS.Abstract;
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

            int pageSize = 9;
            double pages = Math.Ceiling((double)members.Count() / pageSize);

            int pageNum = page ?? 1;

            ViewBag.Pages = pages;

            var membersPaged = members.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();

            return View(membersPaged);
        }

        /// <summary>
        /// GET: Details
        /// Displays a member's details based on the membership level of the
        /// member. This will only send select information to the view to
        /// prevent private information from being displayed.
        /// 
        /// Defaults to ID = 5 for some reason.
        /// </summary>
        /// <param name="ID"> The member ID to display</param>
        /// <returns></returns>
        public ActionResult Details(int? ID)
        {
            if (!ID.HasValue)
            {
                ID = 5; // default member (probably should change this
            }

            Member memb;
            memb = memberRepo.Find(ID);
            if (memb == null)
            {
                ID = 5;
                memb = memberRepo.Find(5);
            }
            ContactInfo cont = new ContactInfo();

            // This should probably be checked in the repo method rather
            // than here, but such is life.
            try
            {
                cont = contactRepo.ListingInfoFromMID(ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            // viewModel for the member to display details for
            MemberDetails memDet = new MemberDetails();
            memDet.Contact = cont;

            memDet.MemberLevelbyInt = memb.MemberLevel;

            //get the abbreviated member level
            string abbr;
            ToAbbr.TryGetValue(memb.MemberLevel1.MLevel, out abbr);
            memDet.LevelAbbrev = ", " + abbr;

            memDet.FullName = memb.FullName;
            memDet.BusinessName = memb.BusinessName;

            // if not a student member, list business and website
            if (memb.MemberLevel > 1)
            {
                memDet.MemberLevel = memb.MemberLevel1.MLevel;
                memDet.Website = memb.Website;
            }
            else // list the school the student attends
            {
                // We have no schools set in database. Maybe require students
                // to list their school as the business name.
                memDet.BusinessName = "Grooming School";
            }
            if (cont.StateName != null)
            {
                memDet.Location = cont.StateName + ", " + cont.Country;
            }
            

            return View(memDet);
        }

        /// <summary>
        /// GET: Member Create
        /// This is the GET for the application form for a user to provide thier basic information in order to apply to become an IPG member.
        /// This from requires the user to have already registered on the website.
        /// </summary>
        /// <returns>A View object for the Member/Apply page or a redirect to their details page if already a member</returns>
        [Authorize]
        public ActionResult Apply()
        {
            var userID = User.Identity.GetUserId();
            // if statement to only allow users that haven't already applied to access the application page
            if (memberRepo.FindByIdentityID(userID) == null) // current Identity user doesn't already have an associated IPG account/application
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

                return RedirectToAction("Index", "Manage", new { success = true });
            }

            return View(newMember);
        }

        /// <summary>
        /// Converts the given Identity Role to a friendly short word.
        /// </summary>
        Dictionary<String, String> ToAbbr = new Dictionary<string, string>()
        {
            {"Student Member", "Student" },
            {"IPG Member", "IPG Member" },
            {"Certified Professional Groomer", "CPG" },
            {"Certified Advanced Professional Groomer", "APG" },
            {"International Certified Master Groomer", "ICMG" },
            {"Approved Salon", "Salon" },
            {"Approved School", "School" },
            {"Member School", "School" },
            {"Uncategorized", "Novice" }
        };
    }
}