using CSharpFunctionalExtensions;

namespace HonyStore.Domain.Products
{
    public record ProductPrice
    {
        private ProductPrice(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; init; }

        public static Result<ProductPrice> Create(decimal price)
        {
            if (price <= 0)
            {
                return Result.Failure<ProductPrice>("Цена должна быть положительной");
            }

            return Result.Success(new ProductPrice(price));
        }
        public Result<ProductPrice> ChangePrice(decimal newPrice)
        {
            return Create(newPrice);
        }

        public static implicit operator decimal(ProductPrice price) => price.Value; 
    }
}
