using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Domain.Products.Rules
{
    internal class ProductPriceMustBePositiveRule : IBusinessRule
    {
        readonly double price;
        public ProductPriceMustBePositiveRule(double price) 
        {
            this.price = price;
        }
        public string Message => "Price must be positive";

        public bool IsBroken()
        {
            return price < 0;
        }
    }
}
