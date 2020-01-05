using BusinessLayer;
using HRManagementEntities;
using System.Web.Mvc;

namespace HRManagement.Controllers
{
    [Authorize(Roles = "A")]
    public class UsersController : Controller
    {
        UserManager userManager = new UserManager();

        public ActionResult Index()
        {
            var model = userManager.GetUsers();
            return View(model);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Users user)
        {
            Users model = user;
            return View(model);
        }
        public ActionResult Update(int id)
        {
            Users user = userManager.GetUserById(id);
            return View("Update", user);
        }
        public ActionResult Save(Users user)
        {
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (user.Id > 0)
                    {
                        Users data = userManager.GetUserById(user.Id);
                        data.UserName = user.UserName;
                        data.Password = user.Password;
                        data.Role = user.Role.ToUpper();
                        userManager.Update(data);
                    }
                    else
                        userManager.Save();
                }
                else
                {
                    ModelState.AddModelError("Invalid", "");
                    return View("New", user);
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                userManager.Delete(id);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}