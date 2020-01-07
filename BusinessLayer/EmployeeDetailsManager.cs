using HRManagementDataAccessLayer;
using HRManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EmployeeDetailsManager
    {
        Repository<EmployeeDetails> repo = new Repository<EmployeeDetails>();
        public List<EmployeeDetails> Get()
        {
            return repo.List();
        }
        public EmployeeDetails GetById(int id)
        {
            return repo.Find(id);
        }
        public List<EmployeeDetails> Include(string path)
        {
            return repo.Include(path);
        }
        public int Update(EmployeeDetails employeeDetails)
        {
            return repo.Update(employeeDetails);
        }
        public void Save()
        {
            repo.Save();
        }
        public void Delete(EmployeeDetails employeeDetails)
        {
            repo.Delete(employeeDetails);
        }
    }
}
