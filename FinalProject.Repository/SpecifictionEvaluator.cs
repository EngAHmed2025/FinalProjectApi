using FinalProject.Core.Models;
using FinalProject.Core.Specifictions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Repository
{
    internal static class SpecifictionEvaluator <TEntity> where TEntity : BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InnerQuery, ISpecifictions<TEntity> spec)
        {
            var Query = InnerQuery;

            if (spec.Critria is not null) 
            {
                Query = Query.Where(spec.Critria);
            }

            if(spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc is not null) 
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            
            }

            if (spec.IsPaginationEnabled)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }

            Query = spec.Includes.Aggregate(Query, (CurrrnyQuery, IncludesExpression) => CurrrnyQuery.Include(IncludesExpression));

            return Query;
        }
         
    }
}
