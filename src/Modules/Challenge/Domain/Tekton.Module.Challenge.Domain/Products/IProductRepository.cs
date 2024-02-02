using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken = default);

        Task AddAsync(Product product, CancellationToken cancellationToken = default);

        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    }
}
