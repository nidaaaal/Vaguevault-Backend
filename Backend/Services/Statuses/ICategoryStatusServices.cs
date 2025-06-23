using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Services.Statuses
{
    public interface ICategoryStatusServices
    {
        Task<IEnumerable<Categories>> ViewCategories();

        Task<IEnumerable<Status>> ViewStatus();

        Task<Categories> AddCategory( string name);

        Task<Status> AddStatus(string name);

        Task<bool> DeleteCategory(int id);

        Task<bool> DeleteStatus(int id);

    }
}
