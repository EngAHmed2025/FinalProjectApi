using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Specifictions
{
    public class BaseSpecifictions<T> : ISpecifictions<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get ; set; }
        public int Skip { get ; set ; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get; set; } = false;
        public BaseSpecifictions(Expression<Func<T,bool>> critriaExpression)
        {
            Critria = critriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {

        OrderBy = orderByExpression; 
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {

            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
