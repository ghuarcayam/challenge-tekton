using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Contract;

namespace Tekton.Module.Challenge.Application.Products.CreateProduct
{
    public class CreateProductCommand: ICommand<OperationResult<Guid>>
    {
        public CreateProductCommand(string name, int stock, string description, double price)
        {
            Name = name;
            Stock = stock;
            Description = description;
            Price = price;
        }

        public string Name { get; }
        public int Stock { get; }
        public string Description { get; }
        public double Price { get; }
    }
}
