using BusinessLayer;
using HRManagementEntities;
using HRManagementEntities.ValueObjects;
using System.Linq;
using System.Web.Mvc;

namespace HRManagement.Controllers
{
    public class EmployeesController : Controller
    {
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager departmentManager = new DepartmentManager();
        public ActionResult Index()
        {
            var model = employeeManager.Include("Department");
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
                Departments = new SelectList(departmentManager.Get(), "Id", "Name"),
                DepartmentId = -1
            };

            return View(viewModel);
        }

        [Authorize(Roles = "A,T")]
        [HttpPost]
        //If there is a problem on new data
        public ActionResult New(NewEmployeeViewModel model)
        {
            model.Departments = new SelectList(departmentManager.Get(), "Id", "Name");
            return View(model);
        }

        [Authorize(Roles = "A,T")]
       // [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            var data = employeeManager.GetById(id);

            var model = new NewEmployeeViewModel()
            {
                Employee = data,
                DepartmentId = data.DepartmentId,
                Departments = new SelectList(departmentManager.Get()/*db.Departments*/, "Id", "Name", data.DepartmentId)

            };

            return View(model);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employees employee)
        {

            var department = departmentManager.GetDepartmentById(employee.DepartmentId);
              
            if (ModelState.IsValid)
            {
                //Update
                if (employee.Id > 0)
                {
                    var data = employeeManager.GetById(employee.Id); //db.Employees.Single(x => x.Id == employee.Id);
                    data.Name = employee.Name;
                    data.Surname = employee.Surname;
                    data.DepartmentId = employee.DepartmentId;
                    employeeManager.Update(employee);
                }
                //New Employee
                else   
                employeeManager.Save(employee);

                //If i do not convert to list it throws an exception.
                var model = employeeManager.Include("Department");  //db.Employees.Include("Department").ToList();
                return View("Index", model);
            }
            else
                ModelState.AddModelError("Invalid", "");

            var newEmployee = new NewEmployeeViewModel()
            {
                Employee = employee,
                Departments = new SelectList(departmentManager.Get(), "Id", "Name"),
                DepartmentId =employee.DepartmentId
            };

            return View("New", newEmployee);
        }

        [Authorize(Roles = "A,T")]
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            if (id > 0)
            {
                employeeManager.Delete(id);
            }
            else
                return HttpNotFound();

            var model = employeeManager.Include("Department");
            return View("Index", model);

            //if (id > 0)
            //{
            //    var data = db.Employees.Single(x => x.Id == id);

            //    if (data != null)
            //    {
            //        db.Employees.Remove(data);
            //        db.SaveChanges();
            //    }
            //    else
            //        return HttpNotFound();
            //}

            //var model = db.Employees.Include("Department").ToList();
            //return View("Index", model);
        }
    }
}