using FinalProject.Core.Models;
using FinalProject.Core.Order_Aggregrate;
using FinalProject.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public interface IUnitofWork : IAsyncDisposable


    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
