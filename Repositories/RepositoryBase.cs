using Microsoft.EntityFrameworkCore;
using ACRPhone.Webhook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ACRPhone.Webhook.Repositories
{
    public class RepositoryBase<T, TId> where T : class
    {
        private readonly WebhookContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected WebhookContext DbContext => _dbContext;

        protected RepositoryBase(WebhookContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();

            foreach (var obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public virtual T GetById(TId id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
