using BusinessLayer;
using HRManagementEntities;
using HRManagementEntities.ValueObjects;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HRManagement.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        EmployeeDetailsManager employeeDetailsManager = new EmployeeDetailsManager();
        EmployeeManager employeeManager = new EmployeeManager();
        CitiesManager citiesManager = new CitiesManager();
        CountriesManager CountriesManager = new CountriesManager();
        public ActionResult Index(int id)
        {

            Employees employee = employeeManager.FindAndInclude(id, "Department");
            //EmployeeDetails data = db.EmployeeDetails.FirstOrDefault(x => x.Employees.Id == id); 21/11/2019

            EmployeeDetails data = employeeDetailsManager.FindAndIncludeByEmployeeId(id, "City", "Country");
                //db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == id); 08/01/2020

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
                Cities = new SelectList(citiesManager.Get(), "Id", "Name", "Chose a City"),
                Countries = new SelectList(CountriesManager.Get(), "Id", "Name", "Chose a Country")
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
                Cities = new SelectList(citiesManager.Get(), "Id", "Name", "Chose a City"),
                Countries = new SelectList(CountriesManager.Get(), "Id", "Name", "Chose a Country")
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
                Cities = new SelectList(citiesManager.Get(), "Id", "Name", "Chose a City"),
                Countries = new SelectList(CountriesManager.Get(), "Id", "Name", "Chose a Country")
            };

            return View(viewModel);
        }

        [Authorize(Roles = "A,T")]
      //  [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {

            EmployeeDetails employeeDetails = employeeDetailsManager.FindAndInclude(id, "City", "Country");
               // db.EmployeeDetails.Include("City").Include("Country").Include("Employees").FirstOrDefault(x => x.Id == id);// 08/01/2020
            int employeeId = employeeDetails.Employees.Id;

          //  var employee = employeeManager.FindAndInclude(id, "Department");
            //db.Employees.Include("Department").FirstOrDefault(x => x.Id == employeeId);// 08/01/2020

            EmployeeDetailsViewModel data = new EmployeeDetailsViewModel()
            {
                EmployeeDetail = employeeDetails,
                Employee = employeeDetails.Employees,
                Cities = new SelectList(citiesManager.Get(), "Id", "Name", employeeDetails.CityId),
                Countries = new SelectList(CountriesManager.Get(), "Id", "Name", employeeDetails.CountryId)
            };

            return View(data);
        }

        [Authorize(Roles = "A,T")]
       // [ValidateAntiForgeryToken]
        public ActionResult Save(EmployeeDetailsViewModel employeeDetails)
        {
            //if not do ModelState is false
            int employeeId = Convert.ToInt32(HttpContext.Session["EmployeeId"]);
            employeeDetails.Cities = new SelectList(citiesManager.Get(), "Id", "Name");
            employeeDetails.Countries = new SelectList(CountriesManager.Get(), "Id", "Name");

            if (ModelState.IsValid)
            {

                //Update Employee Detail
                if (employeeDetails.EmployeeDetail.Id > 0)
                {
                    var data = employeeDetailsManager.Find(employeeDetails.EmployeeDetail.Id);
                        //db.EmployeeDetails.Find(employeeDetails.EmployeeDetail.Id);//08/01/2020
                    data.BirthDate = employeeDetails.EmployeeDetail.BirthDate;
                    data.MartialStatus = employeeDetails.EmployeeDetail.MartialStatus;
                    data.Email = employeeDetails.EmployeeDetail.Email;
                    data.Address = employeeDetails.EmployeeDetail.Address;
                    data.CityId = employeeDetails.EmployeeDetail.CityId;
                    data.CountryId = employeeDetails.EmployeeDetail.CountryId;
                    data.PostCode = employeeDetails.EmployeeDetail.PostCode;
                    data.Salary = employeeDetails.EmployeeDetail.Salary;
                    employeeDetailsManager.Update(data);
                    //data.Employees.Id = Convert.ToInt32(HttpContext.Session["EmployeeId"]);
                }

                //New EmployeeDetail
                else
                {
                    employeeDetails.EmployeeDetail.Employees = employeeManager.Find(employeeId);    //db.Employees.Find(employeeId); 08/01/2020
                    employeeDetailsManager.Save(employeeDetails.EmployeeDetail);//db.EmployeeDetails.Add(employeeDetails.EmployeeDetail); 08/01/2020
                }

                //  db.SaveChanges(); 08/01/2020

                EmployeeDetails employeeDetail = employeeDetailsManager.FindAndIncludeByEmployeeId(employeeId, "City", "Country"); //db.EmployeeDetails.Include("City").Include("Country").FirstOrDefault(x => x.Employees.Id == employeeId);

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
      //  [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                EmployeeDetails employeeDetail = employeeDetailsManager.Find(id);//db.EmployeeDetails.FirstOrDefault(x => x.Id == id);

                if (employeeDetail != null)
                {
                    employeeDetailsManager.Delete(employeeDetail);
                    //db.EmployeeDetails.Remove(employeeDetail);
                    //db.SaveChanges();
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