using System.Web.Mvc;

namespace HRManagementEntities.ValueObjects
{
    public class NewEmployeeViewModel
    {
        public Employees Employee { get; set; }
        public int DepartmentId { get; set; }
        public SelectList Departments { get; set; }
    }

}