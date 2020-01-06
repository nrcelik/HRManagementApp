using HRManagementDataAccessLayer;
using HRManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class EmployeeManager
    {
        Repository<Employees> repo = new Repository<Employees>();
        public List<Employees> Get()
        {
            return repo.List();
        }

    }
}
