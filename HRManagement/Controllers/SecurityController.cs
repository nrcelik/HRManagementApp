using BusinessLayer;
using HRManagementEntities;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace HRManagement.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        UserManager userManager = new UserManager();
        public ActionResult Login()
        {      
            if (Session["User"] != null)
            {
                Users user = new Users();
                user.UserName = Session["UserName"].ToString();
                user.Password = Session["Password"].ToString();
                return View();
            }
            else { 
                Users user = new Users();
                return View(user);
            }
 
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            //user.Id = 1;
            // user.Role = "U";

            if (ModelState.IsValid)
            {
                Users userInfo = userManager.GetUser(user.UserName.ToUpper());

                if (userInfo == null)
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
            }
            else
                ModelState.AddModelError("Invalid", "");
                return View(user);


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
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}

