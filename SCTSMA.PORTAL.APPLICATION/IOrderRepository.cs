using SCTSMA.PORTAL.DOMAIN.Order;

namespace SCTSMA.PORTAL.APPLICATION
{
    public interface IOrderRepository
    {
        Task<List<OrderResponseModel>> GetAllOrders();
        Task<OrderResponseModel> CreateOrder(OrderRequestModel order, string token);
        Task<OrderResponseModel> UpdateOrder(OrderRequestModel order, string token, int orderId);
        Task<bool> DeleteOrder(int orderId);
    }
}
