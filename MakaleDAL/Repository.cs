﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MakaleDAL
{
    public class Repository<T> :Singleton, IRepository<T> where T:class
    {        
        private DbSet<T> dbset;
        public Repository()
        {
            dbset = db.Set<T>();
        }
        public List<T> Liste()
        {
           return dbset.ToList();
        }
        public List<T> Liste(Expression<Func<T, bool>> kosul)
        {
            return dbset.Where(kosul).ToList();
        }      
        public int Insert(T nesne)
        {
            dbset.Add(nesne);
            return db.SaveChanges();
        }
        public int Delete(T nesne)
        {
            dbset.Remove(nesne);
            return db.SaveChanges();
        }
        public int Update(T nesne)
        {
            return db.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> kosul)
        {
           return dbset.FirstOrDefault(kosul);
        }
    }
}
