using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TryitterApi.Context;

namespace TryitterApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
      protected MyContext _context;

      public Repository(MyContext contexto)
      {
        _context = contexto;
      }

      public IQueryable<T> Get()
      {
        return _context.Set<T>().AsNoTracking();
      }

      public T GetById(Expression<Func<T, bool>> predicate)
      {
        return _context.Set<T>().SingleOrDefault(predicate);
      }

      public void Add(T entity)
      {
        _context.Set<T>().Add(entity);
      }

      public void Delete(T entity)
      {
            _context.Set<T>().Remove(entity);
      }

      public void Update(T entity)
      {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
      }
    }
}