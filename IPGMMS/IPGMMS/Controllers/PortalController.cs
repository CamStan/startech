﻿using IPGMMS.Abstract;
using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Diagnostics;
using IPGMMS.ViewModels;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

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
            //Set values to pass to the ViewBag
            var user = User.Identity;
            ViewBag.Name = user.Name;

            //Create items to add to the MemberReports ViewModel to pass to the view
            MemberReports reports = new MemberReports();
            reports.MemberCount = memberRepo.GetAllMembers.Count();
            reports.ActiveMemberCount = memberRepo.GetActiveMemberCount();
            reports.NewMemberLast30Count = memberRepo.GetNewMemberCount();
            reports.ExpiredMembersCount = memberRepo.ExpiredMembers.Count();
            reports.ExpiringMembersCount = memberRepo.ExpiringMembers.Count();
            reports.NewMembersCount = memberRepo.NewMembers.Count();
            reports.ExpiringMembers = memberRepo.ExpiringMembers.Take(3);
            reports.NewMembers = memberRepo.NewMembers.Take(3);
            reports.ExpiredMembers = memberRepo.ExpiredMembers.Take(3);
            
            return View(reports);
        }

        //***********************************************LIST MEMBER INFO*****************************
        
        public ActionResult ListMembers()
        {
            var members = memberRepo.GetAllMembers;
            return View(members.ToList());

        }
        

        //***********************************************DETAILED MEMBER INFO*****************************
        public ActionResult DetailMember()
        {
            return View("DetailMember");
        }

        //***********************************************ADD MEMBER***************************************
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
                ContactInfo mail = (ContactInfo)infos.MailingInfo;
                memb.Membership_Number = memberRepo.setMemberNumber(memb, mail);
                memb = memberRepo.InsertorUpdate(memb);
                // Mailing info is required.
                mail = contactRepo.InsertorUpdate(mail);
                contactRepo.LinkMailingContact(memb, mail);

                // Listing info is optional.
                ContactInfo list = infos.ListingInfo;
                list = contactRepo.InsertorUpdate(list);
                contactRepo.LinkListingContact(memb, list);

                Debug.WriteLine("Says it's valid but not really, maybe");
                return RedirectToAction("UpdateMember", new { id = memb.ID });
                //return View("DetailMember", memb);
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
            //Try to get the Listing info from the contact repo. If there's no listing 
            //for this member, then the info.ListingInfo is set to null for this
            //ViewModel that is passed back to the view.
            try
            {
                info.ListingInfo = contactRepo.ListingInfoFromMID(id);
            }
            catch (System.InvalidOperationException)
            {
                info.ListingInfo = null;
            }
            //Try to get the Mailing info from the contact repo. If there's no listing 
            //for this member, then the info.MailingInfo is set to null for this
            //ViewModel that is passed back to the view.
            try
            {
                info.MailingInfo = contactRepo.MailingInfoFromMID(id);
            }
            catch (System.InvalidOperationException)
            {
                info.MailingInfo = null;
            }

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
                return View("Index");
                //return View(Request.UrlReferrer.ToString());
            }


            return View(contactRepo.MailingInfoFromMID(memID));
        }

        // POST: UpdateMemberMailing()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemberMailing(ContactInfo info)
        {
            if (ModelState.IsValid)
            {
                contactRepo.InsertorUpdate(info);
                MemberInfoViewModel memberDetails = new MemberInfoViewModel();
                var memID = contactRepo.getMemberID(info);
                memberDetails.MemberInfo = memberRepo.Find(memID);
                memberDetails.ListingInfo = contactRepo.ListingInfoFromMID(memID);
                memberDetails.MailingInfo = info;
                return View("UpdateMember", memberDetails);
            }
            return View(info);
        }

        // GET: UpdateMemberListing()
        public ActionResult UpdateMemberListing(int? memID)
        {
            if (memID == null)
            {
                return View("Index");
                //return View(Request.UrlReferrer.ToString());
            }

            return View(contactRepo.ListingInfoFromMID(memID));
        }

        // POST: UpdateMemberListing()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemberListing(ContactInfo info)
        {
            if (ModelState.IsValid)
            {
                contactRepo.InsertorUpdate(info);
                MemberInfoViewModel memberDetails = new MemberInfoViewModel();
                var memID = contactRepo.getMemberID(info);
                memberDetails.MemberInfo = memberRepo.Find(memID);
                memberDetails.ListingInfo = info;
                memberDetails.MailingInfo = contactRepo.MailingInfoFromMID(memID);
                return View("UpdateMember", memberDetails);
            }
            return View(info);
        }

        // Test & Certification section to be added to later when time permits.
        public ActionResult ListTests()
        {
            return RedirectToAction("UnderConstruction", "Home");
        }

        public ActionResult AddTest()
        {
            return RedirectToAction("UnderConstruction", "Home");
        }

        public ActionResult ListCertifications()
        {
            return RedirectToAction("UnderConstruction", "Home");
        }

        public ActionResult DetailCertification()
        {
            return RedirectToAction("UnderConstruction", "Home");
        }

        public ActionResult AddCertification()
        {
            return RedirectToAction("UnderConstruction", "Home");
        }

        public ActionResult UpdateCertification()
        {
            return RedirectToAction("UnderConstruction","Home");
        }


        //***************************************REPORTS***********************************************
        /// <summary>
        /// This method is an action result that returns the Report Landing Page view. This is the view
        /// that houses the list of reports available to the admin.
        /// </summary>
        /// <returns>The Report Landing Page View</returns>
        public ActionResult ReportLandingPage()
        {
            return View("ReportLandingPage");
        }
        /// <summary>
        /// This method gets a list of expired members to return to the view.
        /// If there is no data to display, the user is redirected to a custom
        /// error view.
        /// </summary>
        /// <param name="page">The current page number</param>
        /// <param name="sortOrder">The current sort order</param>
        /// <returns>The view with the list of expired members or a custom error view if no data exists.</returns>
        public ActionResult ReportExpiredMembers()
        {
            var expMembersList = memberRepo.ExpiredMembers;

            if (expMembersList == null)
            {
                return View("Error_NoDataFound");
            }
            else
            {
                return View("ReportExpiredMembers", expMembersList.ToList());
            }
        }

        /// <summary>
        /// Get the list of all members who are listed as uncategorized and require admin approval/member level action.
        /// </summary>
        /// <returns>View of all new members</returns>
        public ActionResult ReportNewMember()
        {
            var list = memberRepo.NewMembers;
            if (list == null)
            {
                return View("Error_NoDataFound");
            }
            else
            {
                return View(list.ToList());
            }
        }


        /// <summary>
        /// Get the list of all members who will have a membership lapse in the next two months.
        /// </summary>
        /// <returns>View of all Expiring members</returns>
        public ActionResult ReportExpiringMember()
        {
            var list = memberRepo.ExpiringMembers;
            if (list == null)
            {
                return View("Error_NoDataFound");
            }
            else
            {
                return View(list.ToList());
            }
        }


        /// <summary>
        /// This method exports a list of expired members to Excel.
        /// </summary>
        private void ExportToExcel(IEnumerable<Member> memList, string name)
        {
            var grid = new GridView();
            grid.DataSource = memList;

            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", ("attachment; filename=" + name));
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            grid.RenderControl(htmlWriter);

            Response.Write(stringWriter.ToString());
            Response.End();

        }

        /// <summary>
        /// This method creates an IEnumerable of type Member with all expired members found
        /// in the database and calls the ExportToExcel method to create an Excel file of expired
        /// members.
        /// </summary>
        public void ExpMembersReportToExcel()
        {
            IEnumerable<Member> expMembers = memberRepo.ExpiredMembers;
            ExportToExcel(expMembers, "Expired Members.xls");
        }

        /// <summary>
        /// This method creates an IEnumerable of type Member with all expired members found
        /// in the database and calls the ExportToExcel method to create an Excel file of expired
        /// members.
        /// </summary>
        public void NewMembersReportToExcel()
        {
            IEnumerable<Member> expMembers = memberRepo.NewMembers;
            ExportToExcel(expMembers, "NewMemberReport.xls");
        }
        
        /// <summary>
        /// This method creates an IEnumerable of type Member with all expired members found
        /// in the database and calls the ExportToExcel method to create an Excel file of expired
        /// members.
        /// </summary>
        public void ExpingMembersReportToExcel()
        {
            IEnumerable<Member> expMembers = memberRepo.ExpiringMembers;
            ExportToExcel(expMembers, "ExpiresSoonMemberReport.xls");
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