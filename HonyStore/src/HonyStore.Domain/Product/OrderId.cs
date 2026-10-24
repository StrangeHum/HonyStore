
namespace HonyStore.Domain.Product
{
    public record OrderId
    {
        private OrderId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static OrderId NewOrderId() => new OrderId(Guid.NewGuid());

        public static OrderId Empty() => new OrderId(Guid.Empty);
    }
}
