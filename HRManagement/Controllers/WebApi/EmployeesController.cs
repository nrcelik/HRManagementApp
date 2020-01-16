using BusinessLayer;
using HRManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRManagement.Controllers.WebApi
{
    public class EmployeesController : ApiController
    {
        EmployeeManager employeeManager = new EmployeeManager();
        public IEnumerable<Employees> Get() 
        {
            return employeeManager.Include("Department");  //db.Employees.Include("Department").ToList(); 08/01/2020
        }
        public HttpResponseMessage Get(int id)
        {
            Employees employee = employeeManager.FindAndInclude(id, "Department"); //db.Employees.Include("Department").FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Id");
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }
        public HttpResponseMessage Post(Employees employee)
        {
            try
            {
               int result= employeeManager.Save(employee);

                if (result > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id);
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "insert could not be done.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public HttpResponseMessage Put(Employees employee)
        {
            try
            {
                var data = employeeManager.GetById(employee.Id); //db.Employees.FirstOrDefault(e => e.Id == employee.Id);

                if (data == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee Id : " + employee.Id);
                }
                else
                {
                    data.Name = employee.Name;
                    data.Surname = employee.Surname;
                    data.DepartmentId = employee.DepartmentId;
                }
                
                if (employeeManager.Save() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "update could not be done.");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Employees employee = employeeManager.GetById(id); //db.Employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee could not found");
                }
                else
                {
                    employeeManager.Delete(id);//db.Employees.Remove(employee);

                    if (employeeManager.Save() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Employee Id :" + employee.Id);

                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, " The data could not be deleted.");
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
