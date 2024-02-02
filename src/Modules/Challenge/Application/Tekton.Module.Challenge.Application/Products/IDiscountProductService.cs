using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Application.Products
{
    public interface IDiscountProductService
    {
        Task<int?> Get(Guid productId);
    }
}
