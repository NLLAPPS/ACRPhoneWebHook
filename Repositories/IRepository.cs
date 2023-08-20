using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ACRPhone.Webhook.Repositories
{
    public interface IRepository<T, TId> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(TId id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        void Save();
    }
}
