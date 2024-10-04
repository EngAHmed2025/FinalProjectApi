using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Models
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
