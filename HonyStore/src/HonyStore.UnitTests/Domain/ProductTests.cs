using CSharpFunctionalExtensions;
using HonyStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.v3;

namespace HonyStore.UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Create_WhenNameIsEmpty_ShouldReturnFailure()
        {
            string nameProduct = string.Empty;

            Result<Product> productResult = Product.Create(ProductId.NewProductId(), nameProduct, "картофельная", 300);

            Assert.True(productResult.IsFailure);
            Assert.Equal("Название не может быть пустым", productResult.Error);
        }

        [Fact]
        public void Create_WhenDescriptionIsEmpty_ShouldReturnFailure()
        {
            string descriptionProduct = string.Empty;

            Result<Product> productResult = Product.Create(ProductId.NewProductId(), "Картошка", descriptionProduct, 300);

            Assert.True(productResult.IsFailure);
            Assert.Equal("Описание не может быть пустым", productResult.Error);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_WhenPriceIsNotPositive_ShouldReturnFailure(decimal price)
        {
            Result<Product> productResult = Product.Create(ProductId.NewProductId(), "Картошка", "картофельная", price);

            Assert.True(productResult.IsFailure);
            Assert.Equal("Цена должна быть положительной", productResult.Error);
        }

        [Fact]
        public void Create_WithValidData_ShouldCreateProduct()
        {
            // Arrange
            var name = "Картошка";
            var description = "картофельная";
            var price = 300;
            Result<Product> productResult = Product.Create(ProductId.NewProductId(), name, description, price);

            Assert.True(productResult.IsSuccess);

            Product product = productResult.Value;

            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price.Value);
        }

        [Fact]
        public void ChangePrice_WithValidPrice_ShouldPriceChanged()
        {
            decimal changedValue = 150;
            Result<Product> productResult = Product.Create(ProductId.NewProductId(), "Картошка", "картофельная", 100);
            Product product = productResult.Value;

            product.UpdatePrice(changedValue);

            Assert.Equal(changedValue, product.Price);
        }

        [Fact]
        public void ChangePrice_WhenPriceIsZero_ShouldThrowArgumentException()
        {
            decimal changedValue = 0;
            Result<Product> productResult = Product.Create(ProductId.NewProductId(), "Картошка", "картофельная", 100);
            Product product = productResult.Value;

            var resultChangePrice = product.UpdatePrice(changedValue);

            Assert.True(resultChangePrice.IsFailure);
            Assert.Equal("Цена должна быть положительной", resultChangePrice.Error);
        }
    }
}
