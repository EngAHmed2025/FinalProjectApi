using FinalProject.Core;
using FinalProject.Core.Models;
using FinalProject.Core.Order_Aggregrate;
using FinalProject.Core.Repositories.Contract;
using FinalProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly StoreContext _dbcontext;
        private Dictionary<string, GenericRepository<BaseEntity>> _repositories;

        public UnitOfWork(StoreContext dbcontext)
        {
           _dbcontext = dbcontext;
            _repositories = new Dictionary<string, GenericRepository<BaseEntity>>();
          
        }


        public async Task<int> CompleteAsync()
        {
          return await  _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _dbcontext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if ( _repositories.ContainsKey(key))
            {
                var  repository = new GenericRepository<TEntity>(_dbcontext) as GenericRepository<BaseEntity>;
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
        }
    }
}
