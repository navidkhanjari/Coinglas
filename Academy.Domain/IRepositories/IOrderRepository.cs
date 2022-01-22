using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Order;
using Academy.Domain.Entities.Wallet;
using Academy.Domain.ViewModels.Discount;
using Academy.Domain.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.IRepositories
{
   public interface IOrderRepository
    {
        #region Order
        Task<long> AddSubscribeToOrder(long userId , long SubscribeId);
        Task<long> AddCourseToOrder(long userId , long courseId);
        Task UpdatePriceOrder(long orderId);
        Task AddOrder(Order order);
        Task AddOrderDetails(OrderDetails details);
        Task UpdateOrder(Order order);
        Task<Order> GetOrderForUserPanel(long userId, long orderId);
        Task<Order> GetOrderById(long orderId);
        Task<bool> FinalyOrder(long userId, long orderId);
        Task<List<ShowOrderUserPanelViewModel>> GetUserOrder(long userId);
        #endregion

        #region User
        Task<int> GetBalanceUserWallet( long userId);
        Task<long> AddWalletAsync(Wallet wallet);
        #endregion

        #region Discount
        Task<Discount> GetDiscountByCode(string code);
        Task UpdateDiscount(Discount discount);
        Task<bool> GetUserDiscountCode(long userId, long DiscountId);
        Task AddUserDiscountCode(UserDiscountCode userDiscountCode);
        Task AddDiscount(Discount discount);
        Task<bool> CheckDiscount(string code);
        Task<FilterDiscountViewModel> GetDiscounts(FilterDiscountViewModel filter);
        Task<EditDiscountViewModel> GetDiscountByIdForEdit(long Id);
        Task<Discount> GetDiscountById(long Id);
        #endregion

        #region SaveChanges
        Task SaveChangesAsync();
        #endregion
    }
}
