using HonyStore.Domain.Shared;

namespace HonyStore.Domain.Products
{
    public record ProductId : IEntityId<ProductId>
    {
        private ProductId(Guid value) 
        { 
            Value = value;
        }

        public Guid Value { get; }

        public static ProductId NewId() => new ProductId(Guid.NewGuid());

        public static ProductId Empty() => new ProductId(Guid.Empty);
    }
}
