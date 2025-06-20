using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Wishlists;
using VagueVault.Backend.Repositories.Interface;

namespace VagueVault.Backend.Services.Wishlists
{
    public class WishlistServices:IWishlistServices
    {
        private readonly VagueVaultDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public WishlistServices(VagueVaultDbContext vagueVaultDbContext , IProductRepository productRepository,
            IUserRepository userRepository,IMapper mapper) 
        {   
            _context = vagueVaultDbContext;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
             
        }
        public async Task <string>AddToWishlist(Guid guid, int productId)
        {
           var product = await _productRepository.GetByProductId(productId);
            if (product == null) throw new BadRequestException("Product Not Found on the Corresponding ProductId");

            var user  = await _userRepository.GetUserByGuidAsync(guid);
            if (user == null) throw new UnauthorizedException("USER DOESN'T EXIST");

            var list = new Wishlist
            {
                Id = 0,
                UserId = guid,
                ProductId = productId,
                AddedAt = DateTime.UtcNow
            };
            _context.Wishlist.Add(list);
            await _context.SaveChangesAsync();

            return "Product Added To Wishlist";
        }

        public async Task<IEnumerable<WishlistProductDto>> ViewWishlist(Guid guid)
        {
            List<WishlistProductDto> wishlist = new List<WishlistProductDto>();
            if ((await _userRepository.GetUserByGuidAsync(guid) == null)) throw new UnauthorizedException("USER DOESN'T EXIST");
           var products = await _context.Wishlist.Include(x => x.Products).Where(x => x.UserId == guid).ToListAsync();
            if (products==null) throw new NotFoundException("Your wishlist is empty");
            foreach (var product in products) {
                var wish = new WishlistProductDto
                {
                    Name = product.Products.Name,
                    Price = product.Products.Price,
                    ImageUrl = product.Products.ImageUrl,

                };
                wishlist.Add(wish);
            }
            return wishlist;
        }

        public async Task<string> RemoveFromWishlist(Guid guid, int productId)
        {
           
            var user = await _userRepository.GetUserByGuidAsync(guid);
            if (user == null) throw new UnauthorizedException("No user Found");

            var product = await _productRepository.GetByProductId(productId);
            if (product == null) throw new BadRequestException("Product Not Found on the Corresponding ProductId");


            var wishlist = _context.Wishlist.FirstOrDefaultAsync(x=>x.UserId==guid && x.ProductId==productId);
            if (product == null) throw new NotFoundException("Product Not Found on the Wishlist");

            return "Product removed from Wishlist";

        }
    }
}
