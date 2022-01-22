using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Wallet;
using Academy.Domain.ViewModels.Account;
using Academy.Domain.ViewModels.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface IUserService
    {
        #region User
        Task<RegisterUserResult> RegisterUser(RegisterViewModel register);
        Task<LoginUserResult> LoginUser(LoginViewModel login);
        Task<User> GetUserByEmail(string email);
        Task<bool> ActiveAccount(string activeCode);
        Task<User> GetUserByActiveCode(string activeCode);
        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgotPass);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel ResetPass);
        Task UpdateUser(User user);
        Task<string> GetUserNameById(long userId);
        Task<bool> IsAdmin(string userName);
        Task<int> CountUsers();
        Task<List<User>> GetNewUsersForAdmin();
        #endregion

        #region User Panel
        Task<InformationUserPanelViewModel> GetUserInformationByUserName(string username);
        Task<InformationUserPanelViewModel> GetUserInformationById(long userId);
        Task<ChangeUserPasswordResult> ChangeUserPassword(string username, ChangeUserPasswordViewModel changePass);
        Task ChangePassword(string username, string newPassword);
        #endregion

        #region User Wallet 
        Task<int> GetBalanceUserWallet(string userName);
        Task<List<WalletViewModel>> GetUserWallet(string userName);
        Task<long> GetUserIdByUserName(string userName);
        Task<long> ChargeWallet(string userName, string description,  int amount,bool isPay = false);
        Task<Wallet> GetWalletByWalletId(long walletId);
        Task UpdateWallet(Wallet wallet);

        #endregion

        #region Filter User For Admin Panel
        Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter);
        #endregion

        #region Filter Deleted User For Admin Panel
        Task<FilterUserViewModel> FilterDeletedUsers(FilterUserViewModel filter);
        #endregion

        #region Create User - Admin Panel
        Task<CreateUserResult> CreateUserFromAdmin(CreateUserViewModel user);
        Task AddUserRoles(List<long> roles, long userId);
        #endregion

        #region Edit User- Admin Panel
        Task<EditUserViewModel> GetUserByUserIdForShow(long userId);
        Task<EditUserResult> EditUserFromAdmin(EditUserViewModel editUser);
        #endregion

        #region Delete User - Admin Panel
        Task DeleteUserFromAdmin(long userId);
        #endregion
    }
}
