using Academy.Data.Context;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Wallet;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Account;
using Academy.Domain.ViewModels.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region User
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email.ToLower().Trim());
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }


        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.UserName == userName);
        }

        public async Task<User> CheckUserIsExistAsync(string email, string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.UserName == userName || s.Email == email.ToLower().Trim());
        }
        public async Task<User> GetUserByActiveCodeAsync(string activeCode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActivationCode == activeCode);
        }
        public async Task<User> GetUserByIdAsync(long Id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);
        }


        public async Task<string> GetUserNameById(long userId)
        {
            var user = await GetUserByIdAsync(userId);

            return user.UserName;
        }

        public async Task<bool> IsAdmin(long userId)
        {
            return await _context.UserRoles
                .Where(u => u.UserId == userId && u.RoleId == 1)
                .AnyAsync();
        }

        public async Task<int> CountUsers()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<List<User>> GetNewUsersForAdmin()
        {
            return await _context.Users
                .Take(10).OrderByDescending(u => u.RegisterDate)
                .ToListAsync();
        }
        #endregion

        #region UserPanel

        public async Task<User> GetUserForChangePasswordAsync(string userName, string oldPassword)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName && u.Password == oldPassword);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await SaveChangesAsync();
        }
        #endregion

        #region User Wallet
        public async Task<long> GetUserIdByUserNameAsync(string userName)
        {
            var user = await GetUserByUserNameAsync(userName);

            return user.Id;
        }

        public async Task<int> BalanceUserWallet(string userName)
        {
            var userId = await GetUserIdByUserNameAsync(userName);

            var Deposit = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToList();

            var Withdraw = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToList();

            var Sum = (Deposit.Sum() - Withdraw.Sum());


            return Sum;
        }

        public async Task<List<WalletViewModel>> GetUserWallet(string userName)
        {
            var userId = await GetUserIdByUserNameAsync(userName);

            return _context.Wallets
                .Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel
                {
                    Amount = w.Amount,
                    Description = w.Description,
                    PaymentDate = w.PaymentDate,
                    Type = w.TypeId
                }).ToList();
        }
        public async Task<long> AddWalletAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();

            return wallet.WalletId;
        }
        public async Task<Wallet> GetWalletByWalletIdAsync(long walletId)
        {
            return await _context.Wallets.FindAsync(walletId);
        }
        public async Task UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);

            await SaveChangesAsync();
        }
        #endregion

        #region Filter User For Admin Panel
        public async Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter)
        {
            var query = _context.Users.AsQueryable();

            #region filter

            if (!string.IsNullOrEmpty(filter.UserName))
            {
                query = query.Where(u => EF.Functions.Like(u.UserName, $"%{filter.UserName}%"));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"));
            }
            if (filter.StartRegisterDate != null)
            {
                query = query.Where(u => u.RegisterDate >= filter.StartRegisterDate);
            }
            if (filter.EndRegisterDate != null)
            {
                query = query.Where(u => u.RegisterDate <= filter.EndRegisterDate);
            }
            #endregion

            #region Paging
            await filter.Build(await query.CountAsync()).SetEntities(query);
            #endregion

            return filter;
        }
        #endregion

        #region Filter Deleted User For Admin Panel
        public async Task<FilterUserViewModel> FilterDeletedUsersAsync(FilterUserViewModel filter)
        {
            var query = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);

            #region filter
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"));
            }
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                query = query.Where(u => EF.Functions.Like(u.UserName, $"%{filter.UserName}%"));
            }
            if (filter.StartRegisterDate != null)
            {
                query = query.Where(u => u.RegisterDate >= filter.StartRegisterDate);
            }
            if (filter.EndRegisterDate != null)
            {
                query = query.Where(u => u.RegisterDate <= filter.EndRegisterDate);
            }

            #endregion

            #region Paging
            await filter.Build(await query.CountAsync()).SetEntities(query);
            #endregion

            return filter;
        }
        #endregion

        #region Edit User - Admin Panel
        public async Task<EditUserViewModel> GetUserByUserIdForShowAsync(long userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.Id,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.UserName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
                }).SingleAsync();
        }
        #endregion

        #region SaveChanges
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
     
        #endregion
    }
}
