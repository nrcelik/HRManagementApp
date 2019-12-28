using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRManagement.Models
{
    public class EmployeeDetails
    {
        [Required]
        public int Id { get; set; }

        [Required, Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required, Display(Name = "E-mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Display(Name = "Phone Number"), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required, Display(Name ="Martial Status")]
        public bool MartialStatus { get; set; }

        [Required]
        public  decimal Salary { get; set; }

        [Required, Column("City_Id")]
        public int CityId { get; set; }

        [Required, Column("Country_Id")]
        public int CountryId { get; set; }
        public virtual Cities City { get; set; }
        public virtual Countries Country { get; set; }
        public virtual Employees Employees { get; set; }

    }
}