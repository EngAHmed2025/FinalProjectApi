using FinalProject.Core.Models;
using FinalProject.Core.Repositories.Contract;
using FinalProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext) 
        {
           _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product)){
                return await _dbcontext.Set<T>().ToListAsync();
            }
          return (IEnumerable<T>) await _dbcontext.Set<Product>().Include(P=>P.Brand).Include(P=>P.Category).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {

            if (typeof(T) == typeof(Product))
            {
                return await _dbcontext.Set<Product>().Where(P => P.Id == id).Include(P=>P.Brand).Include(Product=>Product.Category).FirstOrDefaultAsync()as T;
            }
            return await _dbcontext.Set<T>().FindAsync(id);
        }
            
        }
}
