using Demo.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {

        protected readonly ApplicationDbContext _context = context;

        public TEntity? GetById(int id)=> _context.Set<TEntity>().Find(id);
       


        //Get all 
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
            => withTracking ? _context.Set<TEntity>().Where(d => !d.IsDeleted).ToList() :
            _context.Set<TEntity>().AsNoTracking().Where(d => !d.IsDeleted).ToList();

        //Add
        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);

        // Update

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
        // Delete
        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set <TEntity> ();
            foreach (var include in includes)
                query.Include(include);
            return query
            .AsNoTracking()
            .Where(e => !e.IsDeleted)
            .Select(selector)
            .ToList();
        }


        public IQueryable<TEntity> GetAllQueryable() =>
            _context.Set<TEntity>().AsNoTracking().Where(d => !d.IsDeleted);

    }
}
