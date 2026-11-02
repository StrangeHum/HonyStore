using CSharpFunctionalExtensions;

namespace HonyStore.Domain.Products
{
    public class Product : Shared.Entity<ProductId>
    {
        private Product(ProductId productId, string name, string description, ProductPrice price) : base(productId)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public ProductPrice Price { get; private set; }

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
}
