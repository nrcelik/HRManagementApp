using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRManagement.Models
{
    public class Countries
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }
    
    }
}