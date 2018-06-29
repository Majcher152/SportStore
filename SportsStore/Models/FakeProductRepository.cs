using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository /*: IProductRepository*/
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Ball", Price = 25 },
            new Product { Name = "Surfing board", Price = 179 },
            new Product { Name = "Runner's shoes", Price = 95 }
        }.AsQueryable<Product>();
    }
}
