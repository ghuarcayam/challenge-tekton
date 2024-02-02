using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Domain.Products.Rules
{
    internal class ProductMustBeValidMandatoryFieldRule : IBusinessRule
    {
        private readonly Product product;
        public ProductMustBeValidMandatoryFieldRule(Product product) 
        {
            this.product = product;
        }
        public string Message => throw new NotImplementedException();

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(this.product.Name) ||
                   product.Stock < 0 ||
                   product.Price < 0 ||
                   string.IsNullOrEmpty(this.product.Description);

        }
    }
}
