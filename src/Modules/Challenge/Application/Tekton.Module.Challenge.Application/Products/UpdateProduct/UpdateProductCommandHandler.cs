using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Configuration;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Application.Products.UpdateProduct
{
    internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, OperationResult>
    {
        private readonly IProductRepository productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }
        public async Task<OperationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await this.productRepository.GetAsync(request.ProductId);

            product.SetValues(request.Name, request.Description, request.Price, request.Stock, request.Status == StatusProduct.Active.GetHashCode()? StatusProduct.Active: StatusProduct.Inactive);

            await this.productRepository.UpdateAsync(product, cancellationToken);

            return OperationResult.Ok();
        }
    }
}
