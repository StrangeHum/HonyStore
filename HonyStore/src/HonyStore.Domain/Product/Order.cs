namespace HonyStore.Domain.Product
{
    public class Order
    {
        public Guid IdOrder { get; set; }
        private readonly List<Product> _products = [];

        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        public decimal TotalPrice => Products.Sum(product => product.Price);

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
    }
}
