using Domin.Data;
using Infrastructure.ShaerdRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UintOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Dictionary<Type, object> _Repostroty = new Dictionary<Type,object>();
        private readonly AppDbContext _db;
        private IDbContextTransaction _dbTransaction;
        private bool IsDipose;
        public UnitOfWork(AppDbContext db)
        {
            _db=db;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
           _dbTransaction=await _db.Database.BeginTransactionAsync();
            return _dbTransaction;
        }

        public async Task CommitAsync()
        {
           await  _dbTransaction.CommitAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            // hna ana bolh anta 3ndk ale key dh 
            if (_Repostroty.ContainsKey(typeof(T)))
            {
                // lw ah 3ndoh hrg3oh tany 
                return _Repostroty[typeof(T)] as IGenericRepository<T>;
            }
            //tb l2 m4 3ndoh 5las h7oth we arg3oh
            var _repo = new GenericRepository<T>(_db);
            _Repostroty.Add(typeof(T), _repo);
            return _repo;
        }

        public async Task RollBackAsync()
        {
            await _dbTransaction.RollbackAsync(); 
        }
    }
}
