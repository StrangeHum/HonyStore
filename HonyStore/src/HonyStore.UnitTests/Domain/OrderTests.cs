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
            Product.Create(ProductId.NewProductId(), "Картошка", "Картофельная", 42m).Value,
            Product.Create(ProductId.NewProductId(), "Моркофка", "моркофельная", 12m).Value,
            Product.Create(ProductId.NewProductId(), "Машинка", "Машинная", 1000m).Value,
            Product.Create(ProductId.NewProductId(), "Каршинка", "Карширнная", 1300m).Value,
        };
        public static IEnumerable<TheoryDataRow<OrderId, bool>> GetOrderIdsData = [
            new (OrderId.NewOrderId(), true),
            new (OrderId.Empty(), false)
            ];
        
        public static IEnumerable<TheoryDataRow<List<(Product product, int count)>, decimal>> GetProductsData =
        [
            new  (
                new List<(Product product, int count)>
                {
                    (UnitTestProductDataBase[0], 1),
                    (UnitTestProductDataBase[1], 2)
                }, 66),
            new (
                new List<(Product product, int count)>
                {
                    (UnitTestProductDataBase[2], 3),
                    (UnitTestProductDataBase[3], 1)
                }, 4300
            )
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
        public void CalculateTotalPrice_CurrentDataProduct_ShouldReturnTotalPrice(List<(Product product, int count)> products, decimal expectedResultPrice)
        {
            var orderResult = Order.Create(OrderId.NewOrderId(), products);

            Assert.True(orderResult.IsSuccess);

            Order order = orderResult.Value;

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
