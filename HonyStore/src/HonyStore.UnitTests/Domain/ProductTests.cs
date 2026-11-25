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

            Result<Product> productResult = Product.Create(ProductId.NewId(), nameProduct, "картофельная", ProductPrice.Create(300).Value);

            Assert.True(productResult.IsFailure);
            Assert.Equal("Название не может быть пустым", productResult.Error);
        }

        [Fact]
        public void Create_WhenDescriptionIsEmpty_ShouldReturnFailure()
        {
            string descriptionProduct = string.Empty;

            Result<Product> productResult = Product.Create(ProductId.NewId(), "Картошка", descriptionProduct, ProductPrice.Create(300).Value);

            Assert.True(productResult.IsFailure);
            Assert.Equal("Описание не может быть пустым", productResult.Error);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_WhenPriceIsNotPositive_ShouldReturnFailure(decimal price)
        {
            var productPrice = ProductPrice.Create(price);

            Assert.True(productPrice.IsFailure);
            Assert.Equal("Цена должна быть положительной", productPrice.Error);
        }

        [Fact]
        public void Create_WithValidData_ShouldCreateProduct()
        {
            var name = "Картошка";
            var description = "картофельная";
            var price = 300;
            var productPrice = ProductPrice.Create(300).Value;

            Result<Product> productResult = Product.Create(ProductId.NewId(), name, description, productPrice);

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
            var productPrice = ProductPrice.Create(100).Value;

            Result<Product> productResult = Product.Create(ProductId.NewId(), "Картошка", "картофельная", productPrice);
            Product product = productResult.Value;

            product.UpdatePrice(changedValue);

            Assert.Equal(changedValue, product.Price.Value);
        }

        [Fact]
        public void ChangePrice_WhenPriceIsZero_ShouldThrowArgumentException()
        {
            decimal changedValue = 0;
            var productPrice = ProductPrice.Create(100).Value;

            Result<Product> productResult = Product.Create(ProductId.NewId(), "Картошка", "картофельная", productPrice);
            Product product = productResult.Value;

            var resultChangePrice = product.UpdatePrice(changedValue);

            Assert.True(resultChangePrice.IsFailure);
            Assert.Equal("Цена должна быть положительной", resultChangePrice.Error);
        }
    }
}
