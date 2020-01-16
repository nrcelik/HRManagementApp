using System.Web.Mvc;

namespace HRManagementEntities.ValueObjects
{
    public class EmployeeDetailsViewModel
    {
        public EmployeeDetails EmployeeDetail  { get; set; }
        public Employees Employee { get; set; }
        public SelectList Cities { get; set; }
        public SelectList Countries { get; set; }
    }
}