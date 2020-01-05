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
        HrManagementContext db = new HrManagementContext();

        public IEnumerable<Employees> Get() 
        {
            return db.Employees.Include("Department").ToList();

        }
        public HttpResponseMessage Get(int id)
        {
            Employees employee = db.Employees.Include("Department").FirstOrDefault(x => x.Id == id);

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
                db.Employees.Add(employee);
                if (db.SaveChanges() > 0)
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
                var data = db.Employees.FirstOrDefault(e => e.Id == employee.Id);

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
                if (db.SaveChanges() > 0)
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
                Employees employee = db.Employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee could not found");
                }
                else
                {
                    db.Employees.Remove(employee);

                    if (db.SaveChanges() > 0)
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
