using Destify.MovieApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.Core.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T Insert(T obj);
        T Update(T obj);
        void Delete(object id);
        void Save();

        IEnumerable<T> SelectAll(Expression<Func<T, bool>> predicate);
    }
}
