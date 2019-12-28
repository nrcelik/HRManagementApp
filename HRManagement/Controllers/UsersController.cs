using HRManagement.Models;
using System.Linq;
using System.Web.Mvc;

namespace HRManagement.Controllers
{
    [Authorize(Roles = "A")]
    public class UsersController : Controller
    {
        private HrManagementContext db;

        public UsersController()
        {
            db = new HrManagementContext();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        public ActionResult Index()
        {
            var model = db.Users.ToList();

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
            Users user = db.Users.Find(id);

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
                        Users data = db.Users.Find(user.Id);
                        data.UserName = user.UserName;
                        data.Password = user.Password;
                        data.Role = user.Role.ToUpper();
                    }
                    else
                        db.Users.Add(user);
                    db.SaveChanges();
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
                Users user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();

        }
    }
}