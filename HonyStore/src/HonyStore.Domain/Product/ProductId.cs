using HonyStore.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HonyStore.Domain.Product
{
    public record ProductId
    {
        private ProductId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static ProductId NewProductId() => new ProductId(Guid.NewGuid());

        public static ProductId Empty() => new ProductId(Guid.Empty);
    }
}
