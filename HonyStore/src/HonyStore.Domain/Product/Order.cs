
using CSharpFunctionalExtensions;

namespace HonyStore.Domain.Product
{
    public class Order : Shared.Entity<OrderId>
    {
        private Order(OrderId id) : base(id)
        {
            
        }

        private readonly List<Product> _products = [];
        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        public decimal TotalPrice() => Products.Sum(product => product.Price);

        public Order AddProductToOrder(Product product)
        {
            _products.Add(product);

            return this;
        }
        public Order AddProductToOrder(List<Product> products)
        {
            _products.AddRange(products);

            return this;
        }
        
        public static Result<Order> Create(OrderId id)
        {
            //TODO: Добавление даты заказа и деталей заказа
            return new Order(id);
        }
    }
}
