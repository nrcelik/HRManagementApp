using HRManagementDataAccessLayer;
using HRManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class CountriesManager
    {
        Repository<Countries> repo = new Repository<Countries>();
        public List<Countries> Get()
        {
            return repo.List();
        }
    }
}
