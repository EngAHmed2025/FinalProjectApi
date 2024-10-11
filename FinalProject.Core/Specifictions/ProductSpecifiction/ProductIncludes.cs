using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Specifictions.ProductSpecifiction
{
    public class ProductIncludes : BaseSpecifictions<Product>
    {
        public ProductIncludes() : base() 
        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P=>P.Category);

        }

        public ProductIncludes( int id): base(P=>P.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
