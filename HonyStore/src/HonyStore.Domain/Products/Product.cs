using CSharpFunctionalExtensions;
using HonyStore.Domain.Orders;
using HonyStore.Domain.Shared;

namespace HonyStore.Domain.Products
{
    public class Product : Shared.Entity<ProductId>
    {
        //EF core
        private Product() : base() { }
        private Product(ProductId productId, string name, string description, ProductPrice price) : base(productId)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public ProductPrice Price { get; private set; }

        public static Result<Product> Create(ProductId id, string name, string description, ProductPrice price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Product>("Название не может быть пустым");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Product>("Описание не может быть пустым");
            }

            if(name.Length >= Constraints.MAX_LENGTH_TITLE)
            {
                return Result.Failure<Product>($"Название не может быть длиннее {Constraints.MAX_LENGTH_TITLE}");
            }

            if (description.Length >= Constraints.MAX_LENGTH_TITLE)
            {
                return Result.Failure<Product>($"Описание не может быть длиннее {Constraints.MAX_LENGTH_TITLE}");
            }

            Product product = new(id, name, description, price);

            return Result.Success(product);
        }

        public Result<ProductPrice> UpdatePrice(decimal newPrice)
        {
            return ProductPrice.Create(newPrice).Tap(price => Price = price);
        }
    }
}
