using System.Collections.Generic;

namespace HRManagementEntities.ValueObjects
{
    public class GetEmployeeViewModel
    {
        public Employees Employee { get; set; }
        public List<EmployeeDetails> EmployeeDetails { get; set; }

    }
}