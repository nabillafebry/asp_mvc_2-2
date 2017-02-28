﻿using System.Web.Mvc;
using asp_mvc_1.Security;
using System.Web.Security;
using asp_mvc_1.Models.ViewModel;
using asp_mvc_1.Models.EntityManager;

namespace asp_mvc_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }
        [AuthorizeRoles("Admin")]
        public ActionResult AdminOnly()
        {
            return View();
        }
        public ActionResult UnAuthorized()
        {
            return View();
        }
        [AuthorizeRoles("Admin")]
        public ActionResult ManageUserPartial(string status = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                UserManager UM = new UserManager();
                UserDataView UDV = UM.GetUserDataView(loginName);
                string message = string.Empty;
                if (status.Equals("update"))
                    message = "Update Successful";
                else if (status.Equals("delete"))
                    message = "Delete Successful";
                ViewBag.Message = message;
                return PartialView(UDV);
            }
            return RedirectToAction("Index", "Home");
        }
        [AuthorizeRoles("Admin")]
        public ActionResult UpdateUserData(int userID, string loginName, string password,
string firstName, string lastName, string gender, int roleID = 0)
        {
            UserProfileView UPV = new UserProfileView();
            UPV.SYSUserID = userID;
            UPV.LoginName = loginName;
            UPV.Password = password;
            UPV.FirstName = firstName;
            UPV.LastName = lastName;
            UPV.Gender = gender;
            if (roleID > 0)
                UPV.LOOKUPRoleID = roleID;
            UserManager UM = new UserManager();
            UM.UpdateUserAccount(UPV);
            return Json(new { success = true });
        }
    }

}