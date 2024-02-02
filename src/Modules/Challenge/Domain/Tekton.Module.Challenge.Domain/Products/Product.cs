using Tekton.Module.Challenge.Domain.Products.Rules;

namespace Tekton.Module.Challenge.Domain.Products
{
    public class Product: DomainEntity
    {
        private Guid _productId;
        private string _name;
        private int _status;
        private int _stock;
        private string _description;
        private double _price;
        private int _discount;

        private Product() 
        {
            //EF
        }

        public Product(string name, int stock, string description, double price)
        {
            _productId = Guid.NewGuid();
            _name = name;
            _status = StatusProduct.Active.GetHashCode();
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
        public int Discount { get => _discount;  }
        public int Status { get => _status; }

        /// <summary>
        /// Asigna un valor al descuento y retorna el precio final
        /// </summary>
        /// <param name="discount"></param>
        /// <returns>Precio Final</returns>
        public double ApplyDiscount(int discount) 
        {
            this.CheckRule(new ProductDiscountMustBeInAllowedRangeRule(discount));
           this._discount = discount;
           return  this.Price * (100 - discount) / 100;
        }

        public void SetValues(string name, string description, double price, int stock, StatusProduct status) 
        {
            _name = name;
            _status = status.GetHashCode();
            _stock = stock;
            _description = description;
            _price = price;
        }


    }
}
