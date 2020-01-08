using HRManagementDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class CountriesManager
    {
        Repository<CountriesManager> repo = new Repository<CountriesManager>();
        public List<CountriesManager> Get()
        {
            return repo.List();
        }
    }
}
