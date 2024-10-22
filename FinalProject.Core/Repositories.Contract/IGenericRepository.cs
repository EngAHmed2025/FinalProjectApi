using FinalProject.Core.Models;
using FinalProject.Core.Specifictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
     Task<T> GetAsync(int id);
     Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifictions<T> spec);

        Task<T> GetWithSpecAsync(ISpecifictions<T> spec);
    }
}
