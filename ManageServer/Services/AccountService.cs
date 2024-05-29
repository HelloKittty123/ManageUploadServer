using ManageServer.CustomException;
using ManageServer.Entities;
using ManageServer.Helper;
using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly ManageContext _context;

        public AccountService(ManageContext context)
        {
            _context = context;
        }

        public async Task<AccountResponseModel> CreateUserAsync(AccountModel accountModel)
        {
            var account = new Account
            {
                FirstName = accountModel.FirstName,
                LastName = accountModel.LastName,
                Email = accountModel.Email,
                Password = accountModel.Password.EncryptBase64(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return new AccountResponseModel
            {
                Id = account.Id,
                FirstName = accountModel.FirstName,
                LastName = accountModel.LastName,
                Email = accountModel.Email,
            };
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Accounts.FindAsync(id);
            if (user != null)
            {
                _context.Accounts.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AccountResponseModel>> GetAllUserAsync(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                search = "";
            }
            var users = await _context.Accounts.OrderByDescending(a => a.CreatedDate)
                .Where(a => a.FirstName.Contains(search)
                || a.LastName.Contains(search)
                || a.Email.Contains(search))
                .Select(acc => new AccountResponseModel
                {
                    Id = acc.Id,
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                })
                .ToListAsync();

            return users;
        }

        public async Task<UserModel> RegisterUserAsync(AccountModel accountModel)
        {
            var account = new Account
            {
                FirstName = accountModel.FirstName,
                LastName = accountModel.LastName,
                Email = accountModel.Email,
                Password = accountModel.Password.EncryptBase64(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return new UserModel
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                CreatedDate = account.CreatedDate,
                UpdatedDate = account.UpdatedDate,
            };
        }

        public async Task<AccountResponseModel> UpdateUserAsync(AccountModel accountModel)
        {
            var account = await _context.Accounts.Where(a => a.Id == accountModel.Id).SingleOrDefaultAsync();
            if (account != null)
            {
                account.FirstName = accountModel.FirstName;
                account.LastName = accountModel.LastName;
                account.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return new AccountResponseModel
                {
                    Id = account.Id,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                };
            }

            throw new NotFoundUserException($"Not found user with id {accountModel.Id}");

        }

        public async Task UpdatePasswordAsync(UpdatePasswordModel accountModel, Guid userId)
        {

            var account = await _context.Accounts.Where(a => a.Id == userId).SingleOrDefaultAsync();
            if (account != null)
            {
                if (!accountModel.OldPassword.Equals(account.Password.DecryptBase64()))
                {
                    throw new InvalidPasswordException("Old password is not correct");
                }
                else if (accountModel.NewPassword.Equals(account.Password.DecryptBase64()))
                {
                    throw new InvalidPasswordException("New password is the same with the old password");
                }

                account.Password = accountModel.NewPassword.EncryptBase64();
                await _context.SaveChangesAsync();
            }


        }

        public async Task<Account> ValidateUserAsync(LoginModel loginModel)
        {
            var account = await _context.Accounts
                .Where(acc => acc.Email == loginModel.Email)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                throw new NotFoundUserException("The email or password is not correct");
            }
            else
            {
                var password = account.Password.DecryptBase64();
                if (account.Password.DecryptBase64().Equals(loginModel.Password))
                {
                    return account;
                }
                throw new NotFoundUserException("The email or password is not correct");
            }
        }

        public async Task<AccountResponseModel> GetAccountByIdAsync(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                throw new NotFoundUserException($"Not found user with id {id}");
            }
            return new AccountResponseModel
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                CreatedDate = account.CreatedDate,
                UpdatedDate = account.UpdatedDate,
            };
        }


    }
}