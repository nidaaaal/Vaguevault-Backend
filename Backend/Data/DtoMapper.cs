using AutoMapper;
using VagueVault.Backend.Models.Auth;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Data
{
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<RegisterDto,Users>().ReverseMap();
            CreateMap<LoginDto,Users>().ReverseMap();
        }
    }
}
