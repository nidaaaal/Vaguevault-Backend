using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using VagueVault.Backend.Data;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Repositories.Interface;
using VagueVault.Backend.Services.Cart;

namespace VagueVault.Backend.Services.Payment
{
    public class PayPalService: IPayPalService
    {
        private readonly PayPalHttpClient _client;
        private readonly VagueVaultDbContext _dbContext;
        private readonly IPaymentRepository _paymentRepo;
        private readonly ICartServices _cartServices;

        
        public PayPalService(PayPalHttpClient client,VagueVaultDbContext vaultDbContext,IPaymentRepository paymentRepository,ICartServices cartServices) 
        {
            _client = client;
            _dbContext = vaultDbContext;    
            _paymentRepo = paymentRepository;
            _cartServices = cartServices;
        }

        public async Task<string> CreateOrder(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null) throw new NotFoundException("OrderId Not Found");

            var usdAmount = _paymentRepo.ConvertINRtoUSD(order.TotalAmount);
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");

            request.RequestBody(new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = "USD",
                            Value = usdAmount.ToString("F2")
                            }
                    }
                },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = "https://vauguevault.vercel.app/checkoutform",
                    CancelUrl = "https://vauguevault.vercel.app/order"

                }
            });

            var response = await _client.Execute(request); 
            var result = response.Result<Order>();  

            var approvalLink = result.Links.FirstOrDefault(x=>x.Rel== "approve")?.Href;
            if (approvalLink != null)
            {
                var uri = new Uri(approvalLink);
                var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
                var token = queryParams.Get("token");
                order.PaypalOrderId = token;
                order.PaymentStatus = "PENDING";
                await _dbContext.SaveChangesAsync();
                return token ?? throw new NotFoundException("Token not found in approval URL");
            }

            throw new NotFoundException("Approval URL not found");
        }
        public async Task<string> CaptureOrder(String paypalOrderId)
        {

            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.PaypalOrderId == paypalOrderId);
            if (order == null) throw new NotFoundException("Invalid OrderId");

            if (order.PaymentStatus == "COMPLETED")
                return "Order already captured";
            var request = new OrdersCaptureRequest(paypalOrderId);
            request.RequestBody(new OrderActionRequest());

            var response = await _client.Execute(request);    
            var result = response.Result<Order>();

            if (result.Status == "COMPLETED")
            {
                foreach(var item in order.OrderCollections)
                {
                    await _paymentRepo.UpdateStock(item.Id, item.Quantity);
                }
                
                order.StatusId = 8;
                order.PaymentStatus = "PAID";
                
                await _cartServices.ClearCart(order.UserId);
                await _dbContext.SaveChangesAsync();
            }
            return "Payment Successful";
            

        }
    }
}
