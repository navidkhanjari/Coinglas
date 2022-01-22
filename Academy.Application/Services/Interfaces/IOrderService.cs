using Academy.Application.Utils.enums;
using Academy.Domain.Entities.Order;
using Academy.Domain.ViewModels.Discount;
using Academy.Domain.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface IOrderService
    {
        #region Orders
        Task<long> AddSubscribeToOrder(long userId, long SubscribeId);
        Task<long> AddCourseToOrder(long userId, long courseId);
        Task<Order> GetOrderForUserPanel(long userId, long orderId);
        Task<bool> FinalyOrder(long userId, long orderId);
        Task<List<ShowOrderUserPanelViewModel>> GetUserOrder(long userId);
        #endregion

        #region Discount
        Task<DiscountTypes> UseDiscount(long orderId, string code);
        Task<CreateDiscountResult> CreateDiscount(CreateDiscountViewModel discount);
        Task<FilterDiscountViewModel> GetDiscounts(FilterDiscountViewModel filter);
        Task<EditDiscountViewModel> GetDiscountByIdForEdit(long Id);
        Task<EditDiscountResult> EditDiscount(EditDiscountViewModel editDiscount);
        #endregion
    }
}
