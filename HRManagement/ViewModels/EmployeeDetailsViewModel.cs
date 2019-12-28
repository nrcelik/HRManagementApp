using HRManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRManagement.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public EmployeeDetails EmployeeDetail  { get; set; }
        public Employees Employee { get; set; }
        public SelectList Cities { get; set; }
        public SelectList Countries { get; set; }
    }
}