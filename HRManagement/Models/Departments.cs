using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRManagement.Models
{
    public class Departments
    {
        public int Id { get; set; }

        [Required, Display(Name ="Department Name")]
        public string Name { get; set; }

        public virtual List<Employees> Employees { get; set; }
    }
}