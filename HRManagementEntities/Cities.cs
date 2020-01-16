using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRManagementEntities
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Countries Country { get; set; }
    }
}