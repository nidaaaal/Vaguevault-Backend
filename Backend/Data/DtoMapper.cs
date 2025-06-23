using AutoMapper;
using VagueVault.Backend.DTOs.Address;
using VagueVault.Backend.DTOs.Cart;
using VagueVault.Backend.DTOs.Order;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.DTOs.Users;
using VagueVault.Backend.Models.Addresses;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Models.Order;
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
            CreateMap<Products, ProductDto>()
     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
     .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Categories.Name))
     .ReverseMap()
     .ForPath(dest => dest.Status.Name, opt => opt.Ignore()) // optional since ReverseMap can't map to nav properties
     .ForPath(dest => dest.Categories.Name, opt => opt.Ignore());

            CreateMap<Products,ProductAddDto>().ReverseMap();   
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartItemRequestDto,CartItemDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<AddAddressDto, Address>().ReverseMap();
            CreateMap<OrderCollections, OrderCollectionDto>()
                .ForMember(des=>des.ProductName,opt=>opt.MapFrom(src=>src.Products.Name))
                .ReverseMap();

            CreateMap<CartItems,CartItemDto>()
                .ForMember(dest=>dest.ProductName,opt=>opt.MapFrom(src=>src.Product.Name))
                .ForMember(dest=>dest.ImageUrl,opt=>opt.MapFrom(src=>src.Product.ImageUrl))
                .ForMember(dest=>dest.Price,opt=>opt.MapFrom(src=>src.Product.Price))
                .ReverseMap();
            CreateMap<Orders, OrderDto>()
                 .ForMember(dest=>dest.OrderId,opt=>opt.MapFrom(src=>src.Id))
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderCollections))
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethods.Name))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                 .ReverseMap();
            CreateMap<Orders, OrdersDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderCollections))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethods.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ReverseMap();
        }
    }
}
