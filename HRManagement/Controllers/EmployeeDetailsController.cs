using HRManagement.ViewModels;
using HRManagementEntities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HRManagement.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private readonly HrManagementContext db;

        public EmployeeDetailsController() => db = new HrManagementContext();
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public ActionResult Index(int id)
        {

            Employees employee = db.Employees.Include("Department").FirstOrDefault(x => x.Id == id);
            //EmployeeDetails data = db.EmployeeDetails.FirstOrDefault(x => x.Employees.Id == id);
            EmployeeDetails data = db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == id);

            if (employee != null)
            {
                HttpContext.Session["Name"] = employee.Name.ToString();
                HttpContext.Session["Surname"] = employee.Name.ToString();
                HttpContext.Session["DepartmentName"] = employee.Department.Name.ToString();
                HttpContext.Session["EmployeeId"] = employee.Id;

                ViewBag.Name = Session["Name"];
                ViewBag.Surname = Session["Surname"];
                ViewBag.DepartmentName = Session["DepartmentName"];
            }

            if (data != null)
                return View(data);

            return View(new EmployeeDetails());
        }

        [HttpPost]
        public ActionResult Index(EmployeeDetailsViewModel model)
        {
            
            //var employee = db.Employees.Include("Department").FirstOrDefault(x => x.Id == id);
            //var data = db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == id);

            EmployeeDetailsViewModel data = new EmployeeDetailsViewModel()
            {
                EmployeeDetail = model.EmployeeDetail,
                Cities = new SelectList(db.Cities, "Id", "Name", "Chose a City"),
                Countries = new SelectList(db.Countries, "Id", "Name", "Chose a Country")
            };

            return View(data);
        }

        [Authorize(Roles = "A,T")]
        public ActionResult New()
        {
            //int id = Convert.ToInt32(HttpContext.Session["EmployeeId"]);
            // Employees employee = db.Employees.Include("Department").FirstOrDefault(x => x.Id == id);

            // var employeeDetails = db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == id);

            ViewBag.Name = Session["Name"];
            ViewBag.Surname = Session["Surname"];
            ViewBag.DepartmentName = Session["DepartmentName"];

            var viewModel = new EmployeeDetailsViewModel()
            {
                EmployeeDetail = new EmployeeDetails(),
                Cities = new SelectList(db.Cities, "Id", "Name", "Chose a City"),
                Countries = new SelectList(db.Countries, "Id", "Name", "Chose a Country")
            };

            return View(viewModel);
        }

        [Authorize(Roles = "A,T")]
        [HttpPost]
        public ActionResult New(EmployeeDetailsViewModel data)
        {
            //int id = Convert.ToInt32(HttpContext.Session["EmployeeId"]);
            // Employees employee = db.Employees.Include("Department").FirstOrDefault(x => x.Id == id);

            // var employeeDetails = db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == id);

            ViewBag.Name = Session["Name"];
            ViewBag.Surname = Session["Surname"];
            ViewBag.DepartmentName = Session["DepartmentName"];

            var viewModel = new EmployeeDetailsViewModel()
            {
                EmployeeDetail = data.EmployeeDetail,
                Cities = new SelectList(db.Cities, "Id", "Name", "Chose a City"),
                Countries = new SelectList(db.Countries, "Id", "Name", "Chose a Country")
            };

            return View(viewModel);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {

            EmployeeDetails employeeDetails = db.EmployeeDetails.Include("City").Include("Country").Include("Employees").FirstOrDefault(x => x.Id == id);
            int employeeId = employeeDetails.Employees.Id;
            var employee = db.Employees.Include("Department").FirstOrDefault(x => x.Id == employeeId);

            EmployeeDetailsViewModel data = new EmployeeDetailsViewModel()
            {
                EmployeeDetail = employeeDetails,
                Employee = employee,
                Cities = new SelectList(db.Cities, "Id", "Name", employeeDetails.CityId),
                Countries = new SelectList(db.Countries, "Id", "Name", employeeDetails.CountryId)
            };

            return View(data);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmployeeDetailsViewModel employeeDetails)
        {
            //if not do ModelState is false
            int employeeId = Convert.ToInt32(HttpContext.Session["EmployeeId"]);
            employeeDetails.Cities = new SelectList(db.Cities.ToList(), "Id", "Name");
            employeeDetails.Countries = new SelectList(db.Countries.ToList(), "Id", "Name");

            if (ModelState.IsValid)
            {

                //Update Employee Detail
                if (employeeDetails.EmployeeDetail.Id > 0)
                {
                    var data = db.EmployeeDetails.Find(employeeDetails.EmployeeDetail.Id);
                    data.BirthDate = employeeDetails.EmployeeDetail.BirthDate;
                    data.MartialStatus = employeeDetails.EmployeeDetail.MartialStatus;
                    data.Email = employeeDetails.EmployeeDetail.Email;
                    data.Address = employeeDetails.EmployeeDetail.Address;
                    data.CityId = employeeDetails.EmployeeDetail.CityId;
                    data.CountryId = employeeDetails.EmployeeDetail.CountryId;
                    data.PostCode = employeeDetails.EmployeeDetail.PostCode;
                    data.Salary = employeeDetails.EmployeeDetail.Salary;
                    //data.Employees.Id = Convert.ToInt32(HttpContext.Session["EmployeeId"]);
                }

                //New EmployeeDetail
                else
                {
                    employeeDetails.EmployeeDetail.Employees = db.Employees.Find(employeeId);
                    db.EmployeeDetails.Add(employeeDetails.EmployeeDetail);
                }

                db.SaveChanges();

                EmployeeDetails employeeDetail = db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == employeeId);

                if (HttpContext.Session["Name"] != null)
                {
                    ViewBag.Name = Session["Name"];
                    ViewBag.Surname = Session["Surname"];
                    ViewBag.DepartmentName = Session["DepartmentName"];
                }
                return View("Index", employeeDetail);
            }
            else
                ModelState.AddModelError("Invalid", "");

            return View("New", employeeDetails);
        }

        [Authorize(Roles = "A,T")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                EmployeeDetails employeeDetail = db.EmployeeDetails.FirstOrDefault(x => x.Id == id);

                if (employeeDetail != null)
                {
                    db.EmployeeDetails.Remove(employeeDetail);
                    db.SaveChanges();
                }
                else
                    return HttpNotFound();
            }

            EmployeeDetails model = new EmployeeDetails();

            //if session has value, send the value to the view. I checked value by choosing one of them randomly.
            if (HttpContext.Session["Name"] != null)
            {
                ViewBag.Name = Session["Name"];
                ViewBag.Surname = Session["Surname"];
                ViewBag.DepartmentName = Session["DepartmentName"];
            }

            return View("Index", model);
        }
    }
}