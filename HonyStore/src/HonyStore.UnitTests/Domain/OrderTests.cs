using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HonyStore.Domain.Product;
using CSharpFunctionalExtensions;

namespace HonyStore.UnitTests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void Order_CalculateTotalPrice()
        {
            Result<Product> productResult = Product.Create("Картошка", "картофельная", 300);
            Result<Product> product2Result = Product.Create("Моркофка", "моркофельная", 212);

            Assert.True(productResult.IsSuccess);
            Assert.True(product2Result.IsSuccess);

            Order order = new Order();

            order.AddProductToOrder(productResult.Value)
                .AddProductToOrder(product2Result.Value);

            Assert.Equal(512, order.TotalPrice);
        }
    }
}
