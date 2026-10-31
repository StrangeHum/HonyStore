using CSharpFunctionalExtensions;
using HonyStore.Domain.Products;

namespace HonyStore.Domain.Orders
{
    public class OrderItem : ValueObject
    {
        private OrderItem() { }
        internal OrderItem(ProductId productId, decimal price, int count)
        {
            ProductId = productId;
            Count = count;
            Price = price;
        }

        public ProductId ProductId { get; private set; }
        public int Count { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalPrice => Price * Count;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProductId;
            yield return Count;
            yield return Price;
        }
    }
}
