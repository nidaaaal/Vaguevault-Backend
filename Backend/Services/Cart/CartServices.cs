using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Cart;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Models.Product;
using VagueVault.Backend.Repositories.Interface;
using VagueVault.Backend.Services.Product;
using VagueVault.Migrations;

namespace VagueVault.Backend.Services.Cart
{
    
    public class CartServices:ICartServices
    {
        private readonly VagueVaultDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public CartServices(VagueVaultDbContext context ,ICartRepository cartRepository,IMapper mapper,IProductRepository productRepository) 
        {
            _dbContext = context;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<CartItemDto>?> GetCartItems(Guid id)
        {
          var cart = await  _cartRepository.GetCart(id);

            if (cart == null) return null ;
            return _mapper.Map<List<CartItemDto>>(cart.Items);
        }

        public async Task <bool>AddToCart(Guid id,CartItemRequestDto itemRequest)
        {
            var cart = await _cartRepository.GetCart(id);
            if (cart == null)
            {
                cart = new Models.Carts.Cart
                {
                    UserId = id,
                    Items = new List<CartItems>()
                };
              await  _dbContext.Cart.AddAsync(cart);
              await _dbContext.SaveChangesAsync();
            }
           var product = await _dbContext.Products.FirstOrDefaultAsync(p=>p.Id==itemRequest.ProductId);
            if (product == null) throw new NotFoundException("invalid ProductId");
            if (product.Stock < itemRequest.Quantity) throw new BadRequestException("Not Enough Stock!");
          
          var existingProduct = cart.Items.FirstOrDefault(x=>x.ProductId==itemRequest.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Quantity += itemRequest.Quantity;
            }
            else
            {
                var newItem = new CartItems
                {
                    ProductId = itemRequest.ProductId,
                    Quantity = itemRequest.Quantity,
                    AddedAt = DateTime.Now,

                };
                cart.Items.Add(newItem);
            }
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> RemoveFromCart(Guid id, int productId)
        {
            var cart = await _cartRepository.GetCart(id);
            if (cart == null) throw new UnauthorizedException("Cart not found. Please ensure you are logged in!");
            var existingProduct = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            if (existingProduct == null) throw new BadRequestException("Invalid ProductId");
            _dbContext.CartItems.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeceaseQuantity(Guid id, CartItemRequestDto cartItem)
        {
            var cart = await _cartRepository.GetCart(id);
            if (cart == null) throw new UnauthorizedException("Cart not found. Please ensure you are logged in!");
            var existingProduct = cart.Items.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
            if (existingProduct == null) throw new BadRequestException("Invalid ProductId");

            if(existingProduct.Quantity > cartItem.Quantity) { existingProduct.Quantity -= cartItem.Quantity; }

            await _dbContext.SaveChangesAsync();
            return true;


        }

        public async Task<bool> ClearCart(Guid id)
        {
            var cart = await _cartRepository.GetCart(id);
            if (cart == null) throw new NotFoundException("Empty Cart!");
            cart.Items.Clear();
            await _dbContext.SaveChangesAsync();
            return true;
            
        }

    }
}
