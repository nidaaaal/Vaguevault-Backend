namespace VagueVault.Backend.Services.Product
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile file, string folder = "products");
        Task<bool> DeleteImageAsync(string publicId);



    }
}
