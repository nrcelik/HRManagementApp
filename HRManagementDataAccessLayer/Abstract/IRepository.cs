using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementDataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>> where);
        List<T> Include(string path);
        T Find(Expression<Func<T, bool>> where);
        T Find(int id);
        int Save();
        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
    }
}
