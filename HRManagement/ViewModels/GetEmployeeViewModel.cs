using HRManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRManagement.ViewModels
{
    public class GetEmployeeViewModel
    {
        public Employees Employee { get; set; }
        public List<EmployeeDetails> EmployeeDetails { get; set; }

    }
}