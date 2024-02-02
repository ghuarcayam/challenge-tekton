using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Configuration;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Application.Products.GetProduct
{
    internal class GetProductQueryHandler : IQueryHandler<GetProductQuery, OperationResult<GetProductResult>>
    {
        private readonly IProductRepository productRepository;
        private readonly IDiscountProductService discountProductService;
        private readonly IStatusProductService statusProductService;
        public GetProductQueryHandler(IProductRepository productRepository, 
            IDiscountProductService discountProductService,
            IStatusProductService statusProductService)
        {
            this.productRepository = productRepository;
            this.discountProductService = discountProductService;
            this.statusProductService = statusProductService;
        }

        public async Task<OperationResult<GetProductResult>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await this.productRepository.GetAsync(request.ProductId);

            if (product != null) 
            {
                var discount = await  discountProductService.Get(product.ProductId);
                double finalPrice = product.Price;
                if (discount != null) 
                {
                    finalPrice= product.ApplyDiscount(discount.Value);
                }


                var result = new GetProductResult()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Stock = product.Stock,
                    Price = product.Price,
                    Discount = product.Discount,
                    FinalPrice = finalPrice,
                    StatusName = await statusProductService.GetStatusName(product.Status)
                };

                return OperationResult.Ok(result);
            }
            return OperationResult.Ok(default(GetProductResult));


        }
    }
}
