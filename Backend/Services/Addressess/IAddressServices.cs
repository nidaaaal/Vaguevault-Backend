using VagueVault.Backend.DTOs.Address;

namespace VagueVault.Backend.Services.Addressess
{
    public interface IAddressServices
    {
        Task<IEnumerable<AddressDto>?> GetAddress(Guid id);

        Task<AddressDto> AddAddress( Guid id, AddAddressDto address);

        Task<bool> RemoveAddress(Guid id, int addressId);

        Task<AddressDto> UpdateAddress(Guid id, AddressDto address);

    }
}
