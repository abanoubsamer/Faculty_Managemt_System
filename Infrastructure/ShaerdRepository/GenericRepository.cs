using Domin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ShaerdRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly AppDbContext _db;
        private readonly DbSet<T> _Dbset;


        public GenericRepository(AppDbContext db) 
        {
            _db = db;
            _Dbset = _db.Set<T>();
        }

        public async Task Add(T entity)
        {
            try
            {
                  _Dbset.Add(entity);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
            
                Console.WriteLine($"Database update error: {dbEx.InnerException?.Message}");
                throw;             }
            catch (Exception ex)
            {
              
                Console.WriteLine($"General error: {ex.Message}");
                throw;            }

        }

        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }

        public async Task Delete(T entity)
        {
            try
            {
                _Dbset.Remove(entity);
                 await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine( $"{ex}");
            }
          
        }

        public  async Task<T>Find(Expression<Func<T, bool>> match)
        {
            return  await _Dbset.FirstOrDefaultAsync(match);
        }

        

        public  async Task<List<T>>  GetAll()
        {
            try
            {
                return await _Dbset.ToListAsync();
            }
            catch (Exception ex)
            {
                // قم بتسجيل الاستثناء
                Console.WriteLine(ex.Message);
                return new List<T>(); // أو يمكنك إعادة null أو التعامل بطريقة أخرى حسب الحاجة
            }
           
        }

        public IQueryable<T> GetQueryable()
        {
            return  _Dbset.AsQueryable();
        }

        public async Task Update(T entity)
        {
            _Dbset.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
