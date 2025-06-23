using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Order;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Models.Order;
using VagueVault.Backend.Repositories.Interface;
using VagueVault.Backend.Services.Cart;

namespace VagueVault.Backend.Services.Orderss
{
    public class OrderServices:IOrderServices
    {
        private readonly VagueVaultDbContext _dbContext;
        private readonly ICartServices _cartServices;
        private readonly IMapper _mapper;
        public OrderServices(VagueVaultDbContext dbContext, ICartServices cartServices,IMapper mapper)
        {
            _dbContext = dbContext;
            _cartServices = cartServices;   
            _mapper = mapper;   
        }
        public async Task <int>PlaceOrder(Guid id, OrderRequestDto orderDto)
        {
            var cart = await _cartServices.GetCartItems(id) ?? throw new UnauthorizedException("Cart Is Empty Or Please Ensure Login");
            decimal Total = 0;
           
            var order = new Orders
            {
                UserId = id,
                PaymentMethodId = orderDto.PaymentMethodId,
                ShippingAddressId = orderDto.ShippingAddressId,
                OrderDate=DateTime.Now,
                StatusId=7
                
            };
            
            foreach (var item in cart)
            {
                var ItemTotal = item.Quantity * item.Price;

                var orderitem = new OrderCollections
                {
                    OrderId=order.Id,
                    Id=0,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    TotalItemPrice = ItemTotal
                };
              
                Total += ItemTotal;
                order.TotalAmount = Total;


                order.OrderCollections.Add(orderitem);  
            }

           await _dbContext.Orders.AddAsync(order);
           await _dbContext.SaveChangesAsync();
           return order.Id;
        }

        public async Task<IEnumerable<OrderDto>?> ViewOrders(Guid id)
        {
            var data = await _dbContext.Orders
                .Include(o=>o.OrderCollections).ThenInclude(o=>o.Products)
                .Include(o=>o.PaymentMethods)
                .Include(o=>o.Status)
                .Where(o => o.UserId == id).ToListAsync();
            return _mapper.Map<List<OrderDto>>(data);
        }
        public async Task<IEnumerable<OrdersDto>?> ViewUserOrders()
        {
            var data = await _dbContext.Orders
                .Include(o => o.OrderCollections).ThenInclude(o => o.Products)
                .Include(o => o.PaymentMethods)
                .Include(o => o.Status)
                .ToListAsync();
            return _mapper.Map<List<OrdersDto>>(data);
        }
    }

    }

