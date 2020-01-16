using HRManagementDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementDataAccessLayer
{
    public class RepositoryBase
    {
        private static DatabaseContext _db;
        protected RepositoryBase()
        {

        }
        public static DatabaseContext CreateContext()
        {
            if (_db==null)
            {
                _db = new DatabaseContext();
            }
            return _db;
        }
    }
}
