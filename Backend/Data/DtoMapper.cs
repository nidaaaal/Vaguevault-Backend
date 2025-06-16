using AutoMapper;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.DTOs.Users;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Products;
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
        }
    }
}
