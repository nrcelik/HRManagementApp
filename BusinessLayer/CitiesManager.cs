using HRManagementDataAccessLayer;
using HRManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class CitiesManager
    {
        Repository<Cities> repo = new Repository<Cities>();
        public List<Cities> Get()
        {
            return repo.List();
        }

    }
}
