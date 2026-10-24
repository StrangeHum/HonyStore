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
        public static IEnumerable<object[]> GetOrderIdsData()
        {
            yield return new object[] { OrderId.NewOrderId(), true };
            yield return new object[] { OrderId.Empty(), true };
        }
        public static IEnumerable<object[]> GetProductsData()
        {
            yield return new object[] { 
            new[] { 
                Product.Create(ProductId.NewProductId(), "Картошка", "картофельная", 300m).Value,
                Product.Create(ProductId.NewProductId(), "Моркофка", "моркофельная", 212m).Value
            }, 512 };
            yield return new object[] {
            new[] {
                Product.Create(ProductId.NewProductId(), "Картошка", "картофельная", 200m).Value,
                Product.Create(ProductId.NewProductId(), "Моркофка", "моркофельная", 100m).Value
            }, 300 };
        }

        [Theory]
        [MemberData(nameof(GetOrderIdsData))]
        public void Create_VariousOrderIds_ShouldMatchExpectedResult(OrderId id, bool expectedSucces)
        {
            var orderResult = Order.Create(id);

            Assert.Equal(expectedSucces, orderResult.IsSuccess);
        }


        [Theory]
        [MemberData(nameof(GetProductsData))]
        public void CalculateTotalPrice_CurrentDataProduct_ShouldReturnTotalPrice(Product[] products, decimal expectedResultPrice)
        {
            var orderResult = Order.Create(OrderId.NewOrderId());

            Assert.True(orderResult.IsSuccess);

            Order order = orderResult.Value;
            order.AddProductToOrder(products.ToList());

            Assert.Equal(expectedResultPrice, order.TotalPrice());
        }

        [Fact]
        public void Create_CurrentData_ShouldCreateOrder()
        {
            var orderResult = Order.Create(OrderId.NewOrderId());

            Assert.True(orderResult.IsSuccess);
        }
    }
}
