using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Contract;

namespace Tekton.Module.Challenge.Application.Products.GetProduct
{
    public class GetProductQuery: IQuery<OperationResult<GetProductResult>>
    {
        public GetProductQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; } 

    }
}
