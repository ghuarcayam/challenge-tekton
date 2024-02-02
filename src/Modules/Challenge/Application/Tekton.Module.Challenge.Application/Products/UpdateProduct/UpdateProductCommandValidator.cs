using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Application.Products.UpdateProduct
{
    internal class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            var conditions = new List<int>() { StatusProduct.Active.GetHashCode(), StatusProduct.Inactive.GetHashCode() };

            this.RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
            this.RuleFor(x => x.Name).NotNull().NotEmpty();
            this.RuleFor(x => x.Description).NotNull().NotEmpty();
            this.RuleFor(x => x.Stock).GreaterThan(0);
            this.RuleFor(x => x.Price).GreaterThan(0);
            this.RuleFor(x => x.Status).Must(x => conditions.Contains(x)).WithMessage("Please only use: " + String.Join(", ", conditions)); ;
        }
    }
}
