using HRManagementDataAccessLayer;
using HRManagementDataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HRManagementDataAccessLayer
{
    public class Repository<T> : RepositoryBase, IRepository<T>  where T:class
    {
        private DatabaseContext db;
        private DbSet<T> _objectSet;
        public Repository()
        {
            db = RepositoryBase.CreateContext();
            _objectSet = db.Set<T>();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
        public int Save()
        {
            return db.SaveChanges();
        }
        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public int Update(T obj)
        {
            return Save();
        }
        public int Delete (T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }   
    }
}
