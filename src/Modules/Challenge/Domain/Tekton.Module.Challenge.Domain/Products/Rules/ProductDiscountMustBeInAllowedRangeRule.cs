using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Domain.Products.Rules
{
    internal class ProductDiscountMustBeInAllowedRangeRule : IBusinessRule
    {

        const int MAXDISCOUNT = 100;
        const int MINDISCOUNT = 1;

        private readonly int discount;

        public ProductDiscountMustBeInAllowedRangeRule(int discount) => this.discount = discount;
        public string Message => $"discount {discount} must be in the range {MINDISCOUNT} to {MAXDISCOUNT}";

        public bool IsBroken() => !(discount >= MINDISCOUNT && discount <= MAXDISCOUNT);
    }
}
