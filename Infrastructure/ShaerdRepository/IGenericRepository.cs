using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ShaerdRepository
{
    public interface IGenericRepository<T>where T:class
    {
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task<List<T>> GetAll();
        public Task<T> Find(Expression<Func<T,bool>> match);
   
        public IQueryable<T> GetQueryable();
        public IDbContextTransaction BeginTransaction();
        

    }
}
