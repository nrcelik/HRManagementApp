using HRManagement.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace HRManagement.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private HrManagementContext db;

        public SecurityController()
        {
            db = new HrManagementContext();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        public ActionResult Login()
        {
            Users user = new Users();
            //if (Session["UserId"] != null)
            //{
            //    Users user = new Users();
            //    user.UserName = Session["UserName"].ToString();
            //    user.Password = Session["Password"].ToString();
            //    return View();
            //}
            return View(user);

        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            //user.Id = 1;

            Users userInfo = db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (userInfo==null)
            {
                ModelState.AddModelError("", "Invalid Username and Password");
                return View(user);
            }
            else
            {
               
                 Session["User"] = userInfo;
                FormsAuthentication.SetAuthCookie(userInfo.UserName, false);
                return RedirectToAction("Index", "Home");
            }

            //if (ModelState.IsValid)
            //{
                //if (user.RememberMe)
                //{
                //    Session["UserName"] = user.UserName;
                //    Session["Password"] = user.Password;
                //Session["Role"] = user.Role;
                //}         

                //if (userInfo.Id > 0)
                //{
                //    Session["UserId"] = Guid.NewGuid();

                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //    return View(user);

            //}
            //else
            //    ModelState.AddModelError("Invalid", "");
            //return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}

