using VagueVault.Backend.DTOs.Order;

namespace VagueVault.Backend.Services.Orderss
{
    public interface IOrderServices
    {
        Task<int> PlaceOrder(Guid id,OrderRequestDto orderDto);
        Task<IEnumerable<OrderDto>?> ViewOrders(Guid id);
        Task<IEnumerable<OrdersDto>?> ViewUserOrders();

    }
}
