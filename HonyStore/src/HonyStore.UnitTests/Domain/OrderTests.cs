using System;
using System.Collections.Generic;
using System.Text;
using Xunit.v3;
using CSharpFunctionalExtensions;
using HonyStore.Domain.Orders;
using HonyStore.Domain.Products;

namespace HonyStore.UnitTests.Domain
{
    public class OrderTests
    {
        public static List<Product> UnitTestProductDataBase = new List<Product> 
        {
            Product.Create(ProductId.NewId(), "Картошка", "Картофельная", 42m).Value,
            Product.Create(ProductId.NewId(), "Моркофка", "моркофельная", 12m).Value,
            Product.Create(ProductId.NewId(), "Машинка", "Машинная", 1000m).Value,
            Product.Create(ProductId.NewId(), "Каршинка", "Карширнная", 1300m).Value,
        };
        public static IEnumerable<TheoryDataRow<OrderId, bool>> GetOrderIdsData = [
            new (OrderId.NewId(), true),
            new (OrderId.Empty(), false)
        ];
        
        public static IEnumerable<TheoryDataRow<List<InitialOrderItem>, decimal>> GetProductsData =
        [
            new ([new (UnitTestProductDataBase[0], 1), new (UnitTestProductDataBase[1], 2)], 66m),
            new ([new (UnitTestProductDataBase[2], 3), new (UnitTestProductDataBase[3], 1)], 4300m)
        ];

        [Theory]
        [MemberData(nameof(GetOrderIdsData))]
        public void Create_VariousOrderIds_ShouldMatchExpectedResult(OrderId id, bool expectedSucces)
        {
            var orderResult = Order.Create(id);

            Assert.Equal(expectedSucces, orderResult.IsSuccess);
        }


        [Theory]
        [MemberData(nameof(GetProductsData))]
        public void CalculateTotalPrice_CurrentDataProduct_ShouldReturnTotalPrice(List<InitialOrderItem> products, decimal expectedResultPrice)
        {
            var orderResult = Order.Create(OrderId.NewId(), products);

            Assert.True(orderResult.IsSuccess);

            Order order = orderResult.Value;

            Assert.Equal(expectedResultPrice, order.TotalPrice());
        }

        [Fact]
        public void Create_CurrentData_ShouldCreateOrder()
        {
            var orderResult = Order.Create(OrderId.NewId());

            Assert.True(orderResult.IsSuccess);
        }
    }
}
