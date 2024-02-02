using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Configuration;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Application.Products.CreateProduct
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, OperationResult<Guid>>
    {
        private readonly IProductRepository productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository) 
        {
            this.productRepository = productRepository; 
        }
        public async Task<OperationResult<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Stock, request.Description, request.Price);

            await this.productRepository.AddAsync(product);

            return OperationResult.Ok(product.ProductId);
        }
    }
}
