using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Demo.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
       void Add(TEntity entity);
       void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity,bool>> predicate,
           params Expression<Func<TEntity, object >>[] includes);

         IQueryable<TEntity> GetAllQueryable();
        TEntity? GetById(int id);
        void Update(TEntity entity);
    }
}
