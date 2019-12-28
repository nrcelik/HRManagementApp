using HRManagement.Models;
using HRManagement.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HRManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private HrManagementContext db;

        public EmployeesController()
        {
            db = new HrManagementContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult Index()
        {
            var model = db.Employees.Include("Department").ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Employees employee)
        {
            return View(employee);
        }

        [Authorize(Roles = "A,T")]
        public ActionResult New()
        {
            var viewModel = new NewEmployeeViewModel
            {
                Employee = new Employees(),
                Departments = new SelectList(db.Departments.ToList(), "Id", "Name"),
                DepartmentId = -1
            };

            return View(viewModel);
        }

        [Authorize(Roles = "A,T")]
        [HttpPost]
        //If there is a problem on new data
        public ActionResult New(NewEmployeeViewModel model)
        {
            model.Departments = new SelectList(db.Departments.ToList(), "Id", "Name");
            return View(model);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            var data = db.Employees.FirstOrDefault(x => x.Id == id);

            var model = new NewEmployeeViewModel()
            {
                Employee = data,
                DepartmentId = data.DepartmentId,
                Departments = new SelectList(db.Departments, "Id", "Name", data.DepartmentId)

            };
            return View(model);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employees employee)
        {

            var department = db.Departments.Single(x => x.Id == employee.DepartmentId);

            if (ModelState.IsValid)
            {
                //Update
                if (employee.Id > 0)
                {
                    var data = db.Employees.Single(x => x.Id == employee.Id);
                    data.Name = employee.Name;
                    data.Surname = employee.Name;
                    data.DepartmentId = employee.DepartmentId;

                }
                //New Employee
                else
                    db.Employees.Add(employee);
                db.SaveChanges();

                //If i do not convert to list it throws an exception.
                var model = db.Employees.Include("Department").ToList();
                return View("Index", model);
            }
            else
                ModelState.AddModelError("Invalid", "");

            var newEmployee = new NewEmployeeViewModel()
            {
                Employee = employee,
                Departments = new SelectList(db.Departments.ToList(), "Id", "Name"),
                DepartmentId =employee.DepartmentId
            };

            return View("New", newEmployee);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var data = db.Employees.Single(x => x.Id == id);

                if (data != null)
                {
                    db.Employees.Remove(data);
                    db.SaveChanges();
                }
                else
                    return HttpNotFound();
            }

            var model = db.Employees.Include("Department").ToList();
            return View("Index", model);
        }
    }
}