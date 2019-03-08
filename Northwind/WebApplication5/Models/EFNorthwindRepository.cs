using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models
{
    public class EFNorthwindRepository : INorthwindRepository
    {
        private NorthwindContext context;
        public EFNorthwindRepository(NorthwindContext ctx)
        {
            context = ctx;
        }

        // Create iQueryable for Product and Category
        public IQueryable<Product> Product => context.Product;

        public IQueryable<Category> Category => context.Category;
    }
}
