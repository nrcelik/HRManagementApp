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

        //public int Update()
        //{
        //    repoDepartment.Update(Departments department);
        //}

    }
}
