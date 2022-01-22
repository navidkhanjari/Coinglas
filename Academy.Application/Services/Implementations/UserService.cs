using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Application.Utils.Generators;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Wallet;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Account;
using Academy.Domain.ViewModels.Wallet;
using Academy.Application.Senders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Academy.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly IUserRepository _userRepository;
        private readonly IViewRenderService _viewRender;
        private readonly IGoogleRecaptcha _googleRecaptcha;
        private readonly IPermissionRepository _permissionRepository;
        public UserService(IUserRepository userRepository, IViewRenderService viewRender, IGoogleRecaptcha googleRecaptcha, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _viewRender = viewRender;
            _googleRecaptcha = googleRecaptcha;
            _permissionRepository = permissionRepository;
        }


        #endregion

        #region Register user
        public async Task<RegisterUserResult> RegisterUser(RegisterViewModel register)
        {

            if (!await _googleRecaptcha.IsConfirmed())
                return RegisterUserResult.reCaptchaFalse;

            var IsExists = await _userRepository.CheckUserIsExistAsync(register.Email, register.UserName);
            if (IsExists != null) return RegisterUserResult.UserExist;

            var user = new User
            {
                UserName = register.UserName.SanitizeText(),
                Password = HashPassword.EncodePasswordMd5(register.Password),
                ActivationCode = CodeGenerator.GenerateUniqueCode(),
                RegisterDate = DateTime.Now,
                Email = register.Email.ToLower().Trim().SanitizeText(),
                IsActive = false,
                PhoneNumber = register.PhoneNumber.SanitizeText(),
                
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
            #region Send Activation Email

            string bodyEmail = _viewRender.RenderToStringAsync("_ActiveEmail", user);

            SendEmail.Send(user.Email, "فعالسازی حساب کاربری", bodyEmail);
            #endregion
            return RegisterUserResult.Success;

        }
        #endregion

        #region Login user
        public async Task<LoginUserResult> LoginUser(LoginViewModel login)
        {
            if (!await _googleRecaptcha.IsConfirmed())
                return LoginUserResult.reCaptchaFalse;

            var user = await _userRepository.GetUserByEmailAsync(login.Email.SanitizeText());

            string hashPass = HashPassword.EncodePasswordMd5(login.Password);

            if (user == null) return LoginUserResult.UserNotExist;

            if (hashPass != user.Password) return LoginUserResult.WrongPassword;

            if (!user.IsActive) return LoginUserResult.NotActivated;

            return LoginUserResult.Success;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        #endregion

        #region User 
        public async Task<InformationUserPanelViewModel> GetUserInformationByUserName(string userName)
        {
            var user = await _userRepository.GetUserByUserNameAsync(userName);

            InformationUserPanelViewModel informationUser = new InformationUserPanelViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                PhoneNumber =user.PhoneNumber,
                Wallet = await GetBalanceUserWallet(userName)
            };

            return informationUser;
        }

        public async Task<InformationUserPanelViewModel> GetUserInformationById(long userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            InformationUserPanelViewModel informationUser = new InformationUserPanelViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                PhoneNumber = user.PhoneNumber,
                Wallet = await GetBalanceUserWallet(user.UserName)
            };

            return informationUser;
        }

        public async Task<ChangeUserPasswordResult> ChangeUserPassword(string username, ChangeUserPasswordViewModel changePass)
        {
            var user = await _userRepository.GetUserForChangePasswordAsync(username, HashPassword.EncodePasswordMd5(changePass.OldPassword));
            if (user == null)
                return ChangeUserPasswordResult.OldPasswordIsWrong;

            await ChangePassword(username, changePass.Password);

            return ChangeUserPasswordResult.Success;
        }

        public async Task ChangePassword(string username, string newPassword)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);

            user.Password = HashPassword.EncodePasswordMd5(newPassword);

            await _userRepository.UpdateUserAsync(user);
        }
        public async Task<bool> ActiveAccount(string activeCode)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(activeCode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActivationCode = CodeGenerator.GenerateUniqueCode();
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _userRepository.GetUserByActiveCodeAsync(activeCode);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgotPass)
        {
            if (!await _googleRecaptcha.IsConfirmed())
                return ForgotPasswordResult.reCaptchaFalse;

            var user = await _userRepository.GetUserByEmailAsync(forgotPass.Email.SanitizeText());

            if (user == null)
                return ForgotPasswordResult.NotFound;

            #region Send Reset Password Email

            string bodyEmail = _viewRender.RenderToStringAsync("_ResetPassword", user);

            SendEmail.Send(user.Email, "بازیابی کلمه عبور ", bodyEmail);
            #endregion
            return ForgotPasswordResult.Success;

        }
        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel ResetPass)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(ResetPass.ActiveCode);

            if (user == null)
                return ResetPasswordResult.Error;

            var hashNewPass = HashPassword.EncodePasswordMd5(ResetPass.Password);
            user.Password = hashNewPass;

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return ResetPasswordResult.success;

        }
        public async Task<string> GetUserNameById(long userId)
        {
            return  await _userRepository.GetUserNameById(userId);
        }
        public async Task<bool> IsAdmin(string userName)
        {
           var usertId =  await _userRepository.GetUserIdByUserNameAsync(userName);
            return await _userRepository.IsAdmin(usertId);
        }
        public async Task<int> CountUsers()
        {
            return await _userRepository.CountUsers();
        }
        public async Task<List<User>> GetNewUsersForAdmin()
        {
            return await _userRepository.GetNewUsersForAdmin();
        }
        #endregion

        #region User Wallet
        public async Task<int> GetBalanceUserWallet(string userName)
        {
            return await _userRepository.BalanceUserWallet(userName);
        }

        public async Task<List<WalletViewModel>> GetUserWallet(string userName)
        {
            return await _userRepository.GetUserWallet(userName);
        }

        public Task<long> GetUserIdByUserName(string userName)
        {
            return _userRepository.GetUserIdByUserNameAsync(userName);
        }

        public async Task<long> ChargeWallet(string userName, string description, int amount, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                UserId = await GetUserIdByUserName(userName),
                Description = description,
                IsPay = isPay,
                Amount = amount,
                PaymentDate = DateTime.Now,
                TypeId = 1
            };

            return await _userRepository.AddWalletAsync(wallet);

        }

        public async Task<Wallet> GetWalletByWalletId(long walletId)
        {
            return await _userRepository.GetWalletByWalletIdAsync(walletId);
        }

        public async Task UpdateWallet(Wallet wallet)
        {
            await _userRepository.UpdateWallet(wallet);
        }

        #endregion

        #region FilterUser For Admin Panel
        public async Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter)
        {
            return await _userRepository.FilterUsersAsync(filter);
        }
        #endregion

        #region Filter Deleted User For Admin Panel
        public async Task<FilterUserViewModel> FilterDeletedUsers(FilterUserViewModel filter)
        {
            return await _userRepository.FilterDeletedUsersAsync(filter);
        }
        #endregion

        #region Create User - Admin Panel
        public async Task<CreateUserResult> CreateUserFromAdmin(CreateUserViewModel user)
        {

            try
            {
                var IsExists = await _userRepository.CheckUserIsExistAsync(user.Email, user.UserName);
                if (IsExists != null) return CreateUserResult.UserExist;

                var newUser = new User()
                {
                    UserName = user.UserName.SanitizeText(),
                    Email = user.Email.ToLower().Trim().SanitizeText(),
                    Password = HashPassword.EncodePasswordMd5(user.Password),
                    PhoneNumber = user.PhoneNumber,
                    RegisterDate = DateTime.Now,
                    ActivationCode = CodeGenerator.GenerateUniqueCode(),
                    IsActive = true,
                };

                await _userRepository.AddUserAsync(newUser);
                await _userRepository.SaveChangesAsync();
                await AddUserRoles(user.SelectedRoles, newUser.Id);
                await _userRepository.SaveChangesAsync();
                return CreateUserResult.Success;
            }
            catch (Exception)
            {
                return CreateUserResult.Error;
                throw;
            }

        }


        #endregion

        #region Add userRoles
        public async Task AddUserRoles(List<long> roles, long userId)
        {

            if (roles != null && roles.Any())
            {
                foreach (long roleId in roles)
                {
                    await _permissionRepository.AddUserRolesAsync(new UserRole()
                    {
                        RoleId = roleId,
                        UserId = userId
                    });
                }

            }

        }
        #endregion

        #region Edit User- Admin Panel
        public async Task<EditUserViewModel> GetUserByUserIdForShow(long userId)
        {
            return await _userRepository.GetUserByUserIdForShowAsync(userId);
        }
        public async Task<EditUserResult> EditUserFromAdmin(EditUserViewModel editUser)
        {

            var user = await _userRepository.GetUserByIdAsync(editUser.UserId);

            if (user == null)
                return EditUserResult.Error;

            // Edit User

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = HashPassword.EncodePasswordMd5(editUser.Password);
            }
            user.UserName = editUser.UserName.SanitizeText();
            user.Email = editUser.Email.Trim().ToLower().SanitizeText();
            user.PhoneNumber = editUser.PhoneNumber;

            #region EditRoles

            //Delete All User Roles
            await _permissionRepository.DeleteRolesAsync(user.Id);
            //Add New Roles
            await AddUserRoles(editUser.UserRoles, user.Id);

            #endregion

            await _userRepository.UpdateUserAsync(user);

            return EditUserResult.Success;
        }

        #endregion

        #region Delete User - Admin Panel
        public async Task DeleteUserFromAdmin(long userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            user.IsDelete = true;

            await UpdateUser(user);
        }
        #endregion
    }
}
