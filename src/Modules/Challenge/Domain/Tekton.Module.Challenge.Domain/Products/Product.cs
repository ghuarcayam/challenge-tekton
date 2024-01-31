namespace Tekton.Module.Challenge.Domain.Products
{
    public class Product
    {
        private Guid _productId;
        private string _name;
        private StatusProduct _status;
        private int _stock;
        private string _description;
        private double _price;
        private double discount;

        public Product(Guid productId, string name, int stock, string description, double price)
        {
            _productId = productId;
            _name = name;
            _status = StatusProduct.Active;
            _stock = stock;
            _description = description;
            _price = price;
            
        }

        public Guid ProductId { get => _productId;  }
        public string Name { get => _name;  }
        public int Stock { get => _stock;  }
        public string Description { get => _description; }
        public double Price { get => _price; }
        public double Discount { get => discount;  }
        public StatusProduct Status { get => _status; }

        /// <summary>
        /// Asigna un valor al descuento y retorna el precio final
        /// </summary>
        /// <param name="discount"></param>
        /// <returns>Precio Final</returns>
        public double ApplyDiscount(double discount) 
        {
           this.discount = discount;
           return  this.Price * (100 - discount) / 100;
        }


    }
}
