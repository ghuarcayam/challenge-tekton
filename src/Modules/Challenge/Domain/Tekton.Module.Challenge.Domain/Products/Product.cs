using Tekton.Module.Challenge.Domain.Products.Rules;

namespace Tekton.Module.Challenge.Domain.Products
{
    public class Product: DomainEntity
    {
        private Guid _productId;
        private string _name;
        private StatusProduct _status;
        private int _stock;
        private string _description;
        private double _price;
        private int discount;

        public Product(Guid productId, string name, int stock, string description, double price)
        {
            _productId = productId;
            _name = name;
            _status = StatusProduct.Active;
            _stock = stock;
            _description = description;
            _price = price;

            this.CheckRule(new ProductPriceMustBePositiveRule(_price));
            this.CheckRule(new ProductStockMustBePositiveRule(_stock));

        }

        public Guid ProductId { get => _productId;  }
        public string Name { get => _name;  }
        public int Stock { get => _stock;  }
        public string Description { get => _description; }
        public double Price { get => _price; }
        public int Discount { get => discount;  }
        public StatusProduct Status { get => _status; }

        /// <summary>
        /// Asigna un valor al descuento y retorna el precio final
        /// </summary>
        /// <param name="discount"></param>
        /// <returns>Precio Final</returns>
        public double ApplyDiscount(int discount) 
        {
            this.CheckRule(new ProductDiscountMustBeInAllowedRangeRule(discount));
           this.discount = discount;
           return  this.Price * (100 - discount) / 100;
        }


    }
}
