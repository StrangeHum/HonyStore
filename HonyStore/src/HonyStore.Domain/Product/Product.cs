using CSharpFunctionalExtensions;

namespace HonyStore.Domain.Product
{
    public class Product : Shared.Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public decimal Price { get; private set; }

        private Product(ProductId productId, string name, string description, decimal price) : base(productId)
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

            var product = new Product(id, name, description, price);

            return Result.Success(product);
        }


        public Result<decimal> ChangePrice(decimal price)
        {
            if (price <= 0)
            {
                return Result.Failure<decimal>("Цена должна быть положительной");
            }
            Price = price;

            return Result.Success(Price);
        }
    }
}
