using Academy.Application.Services.Interfaces;
using Academy.Application.Utils.enums;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Order;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Discount;
using Academy.Domain.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region constructor

        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #endregion

        #region Orders
        public async Task<long> AddCourseToOrder(long userId, long courseId)
        {
            return await _orderRepository.AddCourseToOrder(userId, courseId);
        }

        public async Task<long> AddSubscribeToOrder(long userId, long SubscribeId)
        {
            return await _orderRepository.AddSubscribeToOrder(userId, SubscribeId);
        }

       

        public async Task<bool> FinalyOrder(long userId, long orderId)
        {
            return await _orderRepository.FinalyOrder(userId, orderId);
        }

        public async Task<Order> GetOrderForUserPanel(long userId, long orderId)
        {
            return await _orderRepository.GetOrderForUserPanel(userId, orderId);
        }

        public async Task<List<ShowOrderUserPanelViewModel>> GetUserOrder(long userId)
        {
            return await _orderRepository.GetUserOrder(userId);
        }
        #endregion

        #region Discount
        public async Task<DiscountTypes> UseDiscount(long orderId, string code)
        {
            var discount = await _orderRepository.GetDiscountByCode(code);
            if (discount == null)
                return DiscountTypes.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DiscountTypes.ExpierDate;

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return DiscountTypes.ExpierDate;


            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountTypes.Finished;


            var order = await _orderRepository.GetOrderById(orderId);

            if (await _orderRepository.GetUserDiscountCode(order.UserId, discount.Id))
                return DiscountTypes.Used;

            int percent = (order.OrderSum * discount.DiscountPercent) / 100;
            order.OrderSum = order.OrderSum - percent;

            await _orderRepository.UpdateOrder(order);

            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }

            await _orderRepository.UpdateDiscount(discount);
            await _orderRepository.AddUserDiscountCode(new UserDiscountCode()
            {
                UserId = order.UserId,
                DiscountId = discount.Id
            });
            await _orderRepository.SaveChangesAsync();

            return DiscountTypes.Success;
        }
        public async Task<CreateDiscountResult> CreateDiscount(CreateDiscountViewModel discount)
        {
           var isExist =  await _orderRepository.CheckDiscount(discount.Code);
            if (isExist)
                return CreateDiscountResult.Exist;

           

            var newDiscount = new Discount()
            {
                DiscountCode = discount.Code.Trim().ToLower(),
                DiscountPercent =discount.DiscountPercent,              
                UsableCount = discount.UsableDiscount,
                StartDate =discount.StartDate,
                EndDate =discount.EndDate
            };
            

            await _orderRepository.AddDiscount(newDiscount);
            await _orderRepository.SaveChangesAsync();
            return CreateDiscountResult.success;

        }

        public async Task<FilterDiscountViewModel> GetDiscounts(FilterDiscountViewModel filter)
        {
            return await _orderRepository.GetDiscounts(filter);
        }

        public async Task<EditDiscountViewModel> GetDiscountByIdForEdit(long Id)
        {
            return await _orderRepository.GetDiscountByIdForEdit(Id);
        }

        public async Task<EditDiscountResult> EditDiscount(EditDiscountViewModel editDiscount)
        {
           var oldDiscount =  await _orderRepository.GetDiscountById(editDiscount.Id);
            if (oldDiscount == null)
                return EditDiscountResult.error;
            //update
            oldDiscount.DiscountCode = editDiscount.Code;
            oldDiscount.DiscountPercent = editDiscount.DiscountPercent;
            oldDiscount.UsableCount = editDiscount.UsableDiscount;
            oldDiscount.StartDate = editDiscount.StartDate;
            oldDiscount.EndDate = editDiscount.EndDate;

            await _orderRepository.UpdateDiscount(oldDiscount);
            return EditDiscountResult.success;
        }
        #endregion
    }
}
