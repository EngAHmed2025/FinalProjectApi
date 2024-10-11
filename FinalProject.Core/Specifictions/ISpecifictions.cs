using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Specifictions
{
    public interface ISpecifictions<T>  where T : BaseEntity 
    {
        public Expression<Func<T,bool>> Critria { get; set; }
        public List< Expression<Func<T, object>>> Includes { get; set; }
    }
}
