using AutoMapper;
using VagueVault.Backend.DTOs.Address;
using VagueVault.Backend.DTOs.Cart;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.DTOs.Users;
using VagueVault.Backend.Models.Addresses;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Models.Product;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Data
{
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<RegisterDto,Users>().ReverseMap();
            CreateMap<LoginDto,Users>().ReverseMap();
            CreateMap<ProductDto,Products>().ReverseMap();
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartItemRequestDto,CartItemDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<AddAddressDto, Address>().ReverseMap();
            CreateMap<CartItems,CartItemDto>()
                .ForMember(dest=>dest.ProductName,opt=>opt.MapFrom(src=>src.Product.Name))
                .ForMember(dest=>dest.ImageUrl,opt=>opt.MapFrom(src=>src.Product.ImageUrl))
                .ReverseMap();
           
        }
    }
}
