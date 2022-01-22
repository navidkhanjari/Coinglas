using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Wallet;
using Academy.Domain.ViewModels.Account;
using Academy.Domain.ViewModels.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Academy.Domain.IRepositories
{
   public interface IUserRepository
    {
        #region User
        Task<User> CheckUserIsExistAsync(string email, string userName);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);
        Task AddUserAsync(User user);
        Task<User> GetUserByActiveCodeAsync(string activeCode);
        Task<User> GetUserByIdAsync(long Id);
        Task<string> GetUserNameById(long userId);
        Task<bool> IsAdmin(long userId);
        Task<int> CountUsers();
        Task<List<User>> GetNewUsersForAdmin();
        #endregion

        #region UserPanel
        Task<User> GetUserForChangePasswordAsync(string userName, string oldPassword);
        Task UpdateUserAsync(User user);
        #endregion

        #region User Wallet
        Task<long> GetUserIdByUserNameAsync(string userName);
        Task<int> BalanceUserWallet(string userName);
        Task<List<WalletViewModel>> GetUserWallet(string userName);
        Task<long> AddWalletAsync(Wallet wallet);
        Task<Wallet> GetWalletByWalletIdAsync(long walletId);
        Task UpdateWallet(Wallet wallet);
        #endregion

        #region Filter User For Admin Panel
        Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter);
        #endregion

        #region Filter Deleted User For Admin Panel
        Task<FilterUserViewModel> FilterDeletedUsersAsync(FilterUserViewModel filter);

        #endregion

        #region Edit User- Admin Panel
        Task<EditUserViewModel> GetUserByUserIdForShowAsync(long userId);
        #endregion

        #region SaveChanges
        Task SaveChangesAsync();
        #endregion

    }
}
