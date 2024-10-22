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
        public ProductIncludes(ProductSpecParams specParams) : base(P =>
            (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower())) &&
            (!specParams.BrandId.HasValue ||P.BrandId == specParams.BrandId.Value) &&
            (!specParams.CategoryId.HasValue || P.CategoryId  == specParams.CategoryId.Value )
            )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

            if (string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }

            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyPagination((specParams.PageIndex -1) * specParams.PagerSize , specParams.PagerSize );

        }

        public ProductIncludes( int id): base(P=>P.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
