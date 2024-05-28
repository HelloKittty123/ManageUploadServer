using ManageServer.Entities;
using ManageServer.Models;

namespace ManageServer.IServices
{
    public interface IAccountService
    {
        Task<Account> ValidateUserAsync(LoginModel loginModel);

        Task<AccountResponseModel> GetAccountByIdAsync(Guid id);

        Task<UserModel> RegisterUserAsync(AccountModel accountModel);

        Task<AccountResponseModel> CreateUserAsync(AccountModel accountModel);

        Task<AccountResponseModel> UpdateUserAsync(AccountModel accountModel);

        Task UpdatePasswordAsync(UpdatePasswordModel accountModel, Guid userId);

        Task DeleteUserAsync(Guid id);

        Task<List<AccountResponseModel>> GetAllUserAsync(string search = "");
    }
}