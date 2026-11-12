using HonyStore.Domain.Products;

namespace HonyStore.Domain.Orders
{
    public record struct InitialOrderItem(Product product, int count);
}
