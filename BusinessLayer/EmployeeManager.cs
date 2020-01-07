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
        public Employees GetById(int id)
        {
            return repo.Find(id);
        }
        public List<Employees> Include(string path)
        {
            return repo.Include(path);
        }
        public int Update(Employees employee)
        {
            return repo.Update(employee);
        }
        public void Save()
        {
            repo.Save();
        }
        public void Delete(int id)
        {
            Employees employee = repo.Find(x => x.Id == id);
            repo.Delete(employee);
        }
    }
}
