using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRManagement.Models
{
    public class Employees
    {
       
        public int Id { get; set; }

        [Required, MaxLength(15)]
        public string Name { get; set; }

        [Required, MaxLength(15)]
        public string Surname { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public virtual Departments Department { get; set; }


    }
}