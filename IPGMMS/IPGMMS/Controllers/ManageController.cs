﻿using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using IPGMMS.Models;
using IPGMMS.Abstract;
using IPGMMS.ViewModels;

namespace IPGMMS.Controllers
{
    [Authorize]
    public class ManageController : MController
    {
        private IMemberRepository memberRepo;
        private IContactRepository contactRepo;

        public ManageController(IMemberRepository mRepo, IContactRepository cRepo)
        {
            memberRepo = mRepo;
            contactRepo = cRepo;
        }
        
        // GET: /Manage/Index
        /// <summary>
        /// This will be the member's private details page.
        /// This should display a message after an identity change, 
        /// and should allow for the linking of identity to DB member.
        /// If member is already linked a viewmodel containing all their information will be passed back to view.
        /// </summary>
        /// <param name="message">optional ManageMessageID The message to be displayed for the user after some change</param>
        /// <param name="success">optional bool if a member has successfully linked an account</param>
        /// <returns></returns>
        public async Task<ActionResult> Index(ManageMessageId? message, bool? success = false)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            // check if a user was just redirected to this page after successfully applying to IPG
            bool applied = success ?? false;
            ViewBag.Success = applied;
            if (applied)
                ViewBag.SuccessMessage = "Thank you for applying to IPG";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            MemberIdentityInfoViewModel modelCompound = new MemberIdentityInfoViewModel();
            modelCompound.MemberInfo = memberRepo.FindByIdentityID(userId);
            if (modelCompound.MemberInfo != null)
            {
                var memId = modelCompound.MemberInfo.ID;
                modelCompound.MailingInfo = contactRepo.MailingInfoFromMID(memId);
                modelCompound.ListingInfo = contactRepo.ListingInfoFromMID(memId);
            }
            modelCompound.IdentityInfo = model;

            return View(modelCompound);

        }
        /// <summary>
        /// This will be the member's private details page.
        /// This should display a message after an identity change.
        /// and should allow for the linking of identity to DB member.
        /// If member is already linked a viewmodel containing all their information will be passed back to view.
        /// </summary>
        /// <param name="modelCompound">MemberIdentityInfoViewModel containing all the member detailed information</param>
        /// <param name="form"></param>
        /// <param name="message">optional ManageMessageID The message to be displayed for the user after some change</param>
        /// <param name="success">optional bool if a member has successfully linked an account</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(MemberIdentityInfoViewModel modelCompound, FormCollection form, ManageMessageId? message, bool? success = false)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            // check if a user was just redirected to this page after successfully applying to IPG
            bool applied = success ?? false;
            ViewBag.Success = applied;

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            modelCompound.IdentityInfo = model;

            string ipgNum = form["membNum"].Equals("") ? "-1" : form["membNum"];

            var mem = memberRepo.FindByIPG_ID(ipgNum);

            int errorCode = 0;
            string errorMessage = "";

            if (mem != null) // IPG number corresponds to a member
            {
                if (mem.Identity_ID == null || mem.Identity_ID == "") // This member doesn't have a corresponding identity account yet
                {
                    // link identity id to member, update role, and populate the rest of the viewModel
                    var level = mem.MemberLevel1.MLevel;
                    level = level.Replace(' ', '_');
                    UserManager.AddToRole(userId, level);

                    mem.Identity_ID = userId;
                    modelCompound.MemberInfo = memberRepo.InsertorUpdate(mem);

                    var memId = modelCompound.MemberInfo.ID;
                    modelCompound.MailingInfo = contactRepo.MailingInfoFromMID(memId);
                    modelCompound.ListingInfo = contactRepo.ListingInfoFromMID(memId);

                    ViewBag.Success = true;
                    ViewBag.SuccessMessage = "Your account has been successfully linked!";

                    return View(modelCompound);
                }
                errorCode = 1;
                errorMessage = "The member number entered is already linked to an account.";
            }
            else
            {
                errorCode = 2;
                errorMessage = "The member number you entered does not exist. Please contact a system admin if you feel this is a mistake.";
            }

            ViewBag.ErrorCode = errorCode;
            ViewBag.ErrorMessage = errorMessage;

            return View(modelCompound);
        }

        //GET: UpdateMyMailing
        /// <summary>
        /// Gets the requested ContactInfo and redirects to the update
        /// ContactInfo page. 
        /// </summary>
        /// <param name="mail">ListingInfo or MailingInfo; the desired contact info</param>
        /// <returns>ContactInfo model for page.</returns>
        public ActionResult UpdateContact(string mail)
        {
            ViewBag.Type = mail;

            Member memb = memberRepo.FindByIdentityID(User.Identity.GetUserId());
            // Pull both ContactInfo out of member object. ContactInfo should 
            // be in the same order for all member objects but checks are
            // there to make sure it is.
            ContactInfo firstInfo;
            ContactInfo secondInfo;
            // Prevents a null ContactInfo from being sent to view.
            ContactInfo NULL = new ContactInfo
            {
                Email = "no value",
                PhoneNumber = "5555551234",
                StreetAddress = "There's none listed",
                City = "Nowhere",
                StateName = "Nope",
                Country = "NO",
                PostalCode = "55555"
            };

            // Try in case the requested contactinfo is null.
            try
            {
                firstInfo = (ContactInfo)memb.Contacts.FirstOrDefault().ContactInfo;
            }
            catch (System.Exception)
            {
                firstInfo = NULL;
            }
            try
            {
                secondInfo = (ContactInfo)memb.Contacts.ElementAt(1).ContactInfo;
            }
            catch (System.Exception)
            {
                secondInfo = NULL;
            }

            // We have the info, send back what is requetsted.
            if (mail == "MailingInfo")
            {
                ViewBag.IsMailing = true;
                if (memb.Contacts.FirstOrDefault().ContactType.ContactType1 == "Mailing")
                {
                    return View(firstInfo);
                }
                else
                {
                    return View(secondInfo);
                }
            }

            if (mail == "ListingInfo")
            {
                ViewBag.IsMailing = false;
                if (memb.Contacts.FirstOrDefault().ContactType.ContactType1 == "Mailing")
                {
                    return View(secondInfo);
                }
                else
                {
                    return View(firstInfo);
                }
            }

            return View("Index", "Home");
        }

        //Post: UpdateMyMailing()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateContact(ContactInfo mailInfo)
        {
            ViewBag.IsMailing = (bool)TempData["IsMailing"];

            if (ModelState.IsValid)
            {
                contactRepo.InsertorUpdate(mailInfo);

                return RedirectToAction("Index");
            }

            return View(mailInfo);
        }

        /// <summary>
        /// Gets the member info based on the currently logged in Member.
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateMyInfo()
        {
            // Find membernumber by the user that is currently logged in.
            // Prevents abuse due to an exposed member ID.
            var test = User.Identity.GetUserId();
            Member memb = memberRepo.FindByIdentityID(User.Identity.GetUserId());

            return View(memb);
        }

        /// <summary>
        /// This method is the POST for UpdateMyInfo(). 
        /// This method will save updates to the database and return the user to the Index page to view 
        /// their changes.
        /// </summary>
        /// <param name="memb">Member object</param>
        /// <returns>If the model state is valid, this returns the user back to the Index view
        /// if it is not valid, it returns the user to the UpdateMyInfo view with the Model given.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMyInfo(Member memb)
        {
            if (ModelState.IsValid)
            {
                memberRepo.InsertorUpdate(memb);
                return RedirectToAction("Index");
            }
            return View(memb);
        }




        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}