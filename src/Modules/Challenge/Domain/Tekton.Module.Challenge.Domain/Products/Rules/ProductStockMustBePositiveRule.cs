using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Domain.Products.Rules
{
    internal class ProductStockMustBePositiveRule: IBusinessRule
    {
        readonly double stock;
        public ProductStockMustBePositiveRule(int stock)
        {
            this.stock = stock;
        }
        public string Message => "Stock must be positive";

        public bool IsBroken()
        {
            return stock < 0;
        }
    }
}
