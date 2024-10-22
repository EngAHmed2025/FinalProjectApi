using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Specifictions.ProductSpecifiction
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        private const int MaxPageSize = 10;

        private int PageSize;

            public int PagerSize 
        {
            get {  return PageSize; } 
            set { PageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        public string? Search { get; set; }
    }
}
