using Destify.MovieApp.Core.Entities;
using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, IAuditEntity
    {
        private readonly MovieAppDbContext _context;
        private readonly DbSet<T> table;

        public GenericRepository(MovieAppDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public T Insert(T obj)
        {
            T entity = table.Add(obj).Entity;
            return entity;
        }

        public T Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return GetById(obj.Id);
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<T> SelectAll(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> searchResult = _context.Set<T>().Where(predicate);
            return searchResult;
        }
    }
}
