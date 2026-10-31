using CSharpFunctionalExtensions;

namespace HonyStore.Domain.Products
{
    public class Product : Shared.Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public ProductPrice Price { get; private set; }

        private Product(ProductId productId, string name, string description, ProductPrice price) : base(productId)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public static Result<Product> Create(ProductId id, string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Product>("Название не может быть пустым");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Product>("Описание не может быть пустым");
            }

            if (price <= 0)
            {
                return Result.Failure<Product>("Цена должна быть положительной");
            }

            var resultPrice = ProductPrice.Create(price);

            if (resultPrice.IsFailure)
            {
                return Result.Failure<Product>(resultPrice.Error);
            }

            var product = new Product(id, name, description, resultPrice.Value);

            return Result.Success(product);
        }


        public Result<ProductPrice> UpdatePrice(decimal newPrice)
        {
            return ProductPrice.Create(newPrice).Tap(price => Price = price);
        }
    }
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
