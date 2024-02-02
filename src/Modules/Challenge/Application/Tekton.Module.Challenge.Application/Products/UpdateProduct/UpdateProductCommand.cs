using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Contract;

namespace Tekton.Module.Challenge.Application.Products.UpdateProduct
{
    public class UpdateProductCommand: ICommand<OperationResult>
    {
        public UpdateProductCommand(Guid productId, string name, int stock, string description, double price, int status)
        {
            ProductId = productId;
            Name = name;
            Stock = stock;
            Description = description;
            Price = price;
            Status = status;
            
        }

        public Guid ProductId { get; }
        public string Name { get; }
        public int Stock { get;  }
        public int Status { get; }
        public string Description { get; }
        public double Price { get; }
    }
}
