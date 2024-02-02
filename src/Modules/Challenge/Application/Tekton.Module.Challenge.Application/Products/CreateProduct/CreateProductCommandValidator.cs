using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Application.Products.CreateProduct
{
    internal class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            this.RuleFor(x=>x.Name).NotNull().NotEmpty();
            this.RuleFor(x => x.Description).NotNull().NotEmpty();
            this.RuleFor(x => x.Stock).GreaterThan(0);
            this.RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
