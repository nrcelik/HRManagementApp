using HRManagementDataAccessLayer;
using HRManagementEntities;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class DepartmentManager
    {
        private Repository<Departments> repoDepartment = new Repository<Departments>();

        public List<Departments> Get()
        {
            return repoDepartment.List();
        }
        public Departments GetDepartmentById(int id)
        {
            return repoDepartment.Find(x => x.Id == id);
        }
        public void Update(Departments department)
        {
            repoDepartment.Update(department);
        }     
        public void Save()
        {
            repoDepartment.Save();
        }
        public int Save(Departments department)
        {
            return repoDepartment.Save(department);
        }
        public void Delete(int id)
        {
            Departments department = repoDepartment.Find(x => x.Id == id);
            repoDepartment.Delete(department);
        }
    }
}
