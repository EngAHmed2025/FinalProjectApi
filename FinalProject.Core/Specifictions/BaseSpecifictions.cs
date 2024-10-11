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
        public BaseSpecifictions()
        {
            
        }
        public BaseSpecifictions(Expression<Func<T,bool>> critriaExpression)
        {
            Critria = critriaExpression;
        }
    }
}
