using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace VLCitas.DataLayer.Tools
{
    interface IRepositoryGeneric<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetByID(Expression<Func<TEntity, bool>> filter = null);
        void Insert(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> filter = null);
        void Update(TEntity entity);
        void Save();
    }


    public class Repository<T> : IRepositoryGeneric<T> where T : class
    {
        private VL_CitasEntities _dbContext;

        public VL_CitasEntities DbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }
        private T _Entity { get; set; }

        public Repository(VL_CitasEntities dbContext = null)
        {
            _dbContext = dbContext ?? new VL_CitasEntities();
        }
        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return this.DbContext.Set<T>();
            return this.DbContext.Set<T>().AsNoTracking().Where(filter);
        }
        public List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return this.DbContext.Set<T>().ToList();
            return this.DbContext.Set<T>().AsNoTracking().Where(filter).ToList();
        }

        public T GetByID(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            return this.DbContext.Set<T>().AsNoTracking().FirstOrDefault(filter);
        }

        public void Insert(T entity)
        {
            this.DbContext.Set<T>().Add(entity);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            this.DbContext.Set<T>().Remove(this.GetByID(filter));
        }

        public void Update(T entity)
        {
            this.DbContext.Set<T>().Attach(entity);
            this.DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            this.DbContext.SaveChanges();
        }
        public int LastID(System.Linq.Expressions.Expression<Func<T, int>> filter)
        {
            int? ID = this.DbContext.Set<T>().Max(filter);
            if (ID == 0 || ID == null)
                return 1;
            else
                return ID.Value + 1;
        }
    }
}