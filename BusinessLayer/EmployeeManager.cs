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
        public Employees Find(int id)
        {
            return repo.Find(x => x.Id == id);
        }
        public Employees FindAndInclude(int id, string path)
        {
            return repo.Find(path, x => x.Id == id);
        }
        public List<Employees> Include(string path)
        {
            return repo.Include(path);
        }
        public int Update(Employees employee)
        {
            return repo.Update(employee);
        }
        public int Save()
        {
            return repo.Save();
        }
        public int Save(Employees employee)
        {
           return repo.Save(employee);
        }
        public void Delete(int id)
        {
            Employees employee = repo.Find(x => x.Id == id);
            repo.Delete(employee);
        }
    }
}
