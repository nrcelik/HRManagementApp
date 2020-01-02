using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRManagement.Models
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Countries Country { get; set; }
    }
}