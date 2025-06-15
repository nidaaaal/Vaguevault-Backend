namespace VagueVault.Backend.Helpers.Auth.Interface
{
    public interface IPasswordValidator
    {

     (bool isValid, string message) ValidatePassword(string password);
        

    }
}
