using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Address;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Addresses;
using VagueVault.Backend.Repositories.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VagueVault.Backend.Services.Addressess
{
    public class AddressServices : IAddressServices
    {
        private readonly IMapper _mapper;
        private readonly VagueVaultDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        public AddressServices(IMapper mapper, VagueVaultDbContext vagueVaultDb, IUserRepository userRepository)
        {
            _dbContext = vagueVaultDb;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<AddressDto>?> GetAddress(Guid id)
        {
            var user = await _userRepository.GetUserByGuidAsync(id);
            if (user == null) throw new UnauthorizedException("Invalid Credential");

            var data = await _dbContext.Address.Where(a => a.UserId == id).ToListAsync();
            return _mapper.Map<List<AddressDto>>(data);
        }

        public async Task<AddressDto> AddAddress(Guid id, AddAddressDto address)
        {
            var user = await _userRepository.GetUserByGuidAsync(id) ?? throw new UnauthorizedException("Invalid Credential");

            var added = _mapper.Map<Address>(address);
            added.UserId = id;
            _dbContext.Address.Add(added);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<AddressDto>(added);



        }

       public async Task<bool> RemoveAddress(Guid id, int addressId)
        {
            var user = await  _userRepository.GetUserByGuidAsync(id) ?? throw new UnauthorizedException("Invalid Credential");
            var address = await _dbContext.Address.FirstOrDefaultAsync(x => x.Id == addressId);
          if (address == null ) throw new BadRequestException("Invalid AddressId");
            _dbContext.Address.Remove(address);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<AddressDto> UpdateAddress(Guid id, AddressDto address)
        {
            var user = await _userRepository.GetUserByGuidAsync(id) ?? throw new UnauthorizedException("Invalid Credential");
            var update = await _dbContext.Address.FirstOrDefaultAsync(x => x.Id == address.Id);
            if (update == null) throw new BadRequestException("Invalid AddressId");
            update.FullName=address.FullName;
            update.PhoneNumber=address.PhoneNumber;
            update.Email=address.Email;
            update.City=address.City;
            update.Street=address.Street; 
            update.PostalCode=address.PostalCode;   
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<AddressDto>(update);

        }

    }
}
