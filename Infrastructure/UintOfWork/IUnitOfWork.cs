using Infrastructure.ShaerdRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UintOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<T> Repository<T>() where T : class ;
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollBackAsync();
        public Task<int> CompleteAsync();
    }
}
