using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Infraestructure.Domain.Products
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ChallengeContext context;
        public ProductRepository(ChallengeContext context) 
        {
            this.context = context;
        }
        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(product, cancellationToken);
        }

        public async Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await this.context.Products.FindAsync(new object[] { productId }, cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
             context.Update(product);
        }
    }
}
