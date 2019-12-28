using HRManagement.Models;
using System.Web.Mvc;

namespace HRManagement.ViewModels
{
    public class NewEmployeeViewModel
    {
        public Employees Employee { get; set; }
        public int DepartmentId { get; set; }
        public SelectList Departments { get; set; }
    }

}