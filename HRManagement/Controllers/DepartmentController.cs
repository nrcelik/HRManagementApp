using BusinessLayer;
using HRManagement.Filters;
using HRManagementEntities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace HRManagement.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        public ActionResult Index()
        {
            List<Departments> model = departmentManager.Get();

            if (model != null)
            {
                return View(model);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Index(Departments department)
        {
            return View(department);
            //var model = db.Departments.ToList();

            //if (model != null)
            //{
            //    return View(model);
            //}
            //else
            //    return HttpNotFound();
        }

        [Authorize(Roles = "A,T")]
        public ActionResult New()
        {
            ViewBag.Message = "New Department";
            return View();
        }

        [Authorize(Roles = "A,T")]
        [HttpPost]
        
        public ActionResult New(Departments department)
        {
            ViewBag.Message = "Update Department";
            return View(department);
        }
        [Authorize(Roles = "A,T")]
        public ActionResult Update(int id)
        {

            var data = db.Departments.First(x => x.Id == id);
            return View(data);
        }

        [Authorize(Roles = "A,T")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Departments department)
        {
            if (ModelState.IsValid)
            {
                if (department.Id > 0)
                {
                    var data = db.Departments.SingleOrDefault(x => x.Id == department.Id);
                    data.Name = department.Name;
                    db.SaveChanges();
                }
                else
                    db.Departments.Add(department);
                db.SaveChanges();

                var model = db.Departments.ToList();

                return View("Index", model);
            }
            else
            {
                ModelState.AddModelError("Invalid", "");
                return View("New");
            }

        }
        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var data = db.Departments.SingleOrDefault(x => x.Id == id);
            if (data != null)
            {
                db.Departments.Remove(data);
                db.SaveChanges();
            }
            else
                return HttpNotFound();
            var model = db.Departments.ToList();

            return View("Index", model);

        }
    }

}