using HonyStore.Domain.Products;
using HonyStore.Domain.Shared;

namespace HonyStore.Domain.Orders
{
    public record OrderId : IEntityId<OrderId>
    {
        private OrderId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static OrderId NewId() => new OrderId(Guid.NewGuid());

        public static OrderId Empty() => new OrderId(Guid.Empty);
    }
}
