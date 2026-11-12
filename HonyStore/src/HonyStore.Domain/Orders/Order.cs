
using CSharpFunctionalExtensions;
using HonyStore.Domain.Products;

namespace HonyStore.Domain.Orders
{
    public class Order : Shared.Entity<OrderId>
    {
        //EF core
        private Order() : base() { }

        private Order(OrderId id, DateTime created) : base(id)
        {
            CreatedDate = created;
        }

        private readonly List<OrderItem> _products = [];
        public IReadOnlyList<OrderItem> Products => _products.AsReadOnly();
        public OrderStatus Status { get; private set; } = OrderStatus.Prepare;
        public DateTime CreatedDate { get; private set; }
        public DateTime? PickupDate { get; private set; }                        // Дата, когда забрали заказ

        public decimal TotalPrice() => _products.Sum(product => product.TotalPrice);

        public Result<Order> AddProductToOrder(Product product, int count)
        {
            if (product == null)
            {
                return Result.Failure<Order>("Продукт пустой");
            }

            if (product.Id == null)
            {
                return Result.Failure<Order>("Пришёл пустой Id продукта");
            }


            //TODO: Изменить логику. Сейчас можно добавлять несколько товаров с одним и тем-же ID
            OrderItem item = new OrderItem(product.Id, product.Price, count);

            _products.Add(item);

            return Result.Success(this);
        }

        public static Result<Order> Create(OrderId id, IReadOnlyList<InitialOrderItem>? initialProducts = null)
        {
            if (id == OrderId.Empty())
            {
                return Result.Failure<Order>("Id заказа пустой");
            }

            Order order = new Order(id, DateTime.UtcNow);

            if (initialProducts != null) 
            {
                foreach (var item in initialProducts) 
                {
                    var addResult = order.AddProductToOrder(item.product, item.count);

                    if (addResult.IsFailure)
                    {
                        return Result.Failure<Order>($"Ошибка при добавлении пунктов заказа:\n{addResult.Error}");
                    }
                }
            }

            return Result.Success(order);
        }
    }
}
