using Academy.Data.Context;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Course;
using Academy.Domain.Entities.Order;
using Academy.Domain.Entities.Subscribe;
using Academy.Domain.Entities.Wallet;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Discount;
using Academy.Domain.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Order
        public async Task<long> AddSubscribeToOrder(long userId, long SubscribeId)
        {
            var order = await _context.Orders
              .FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);

            var subscribe = await _context.Subscribes.FindAsync(SubscribeId);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinally = false,
                    CreateDate = DateTime.Now,
                    OrderSum = subscribe.Price,
                    OrderDetails = new List<OrderDetails>()
                    {
                        new OrderDetails()
                        {
                            SubscribeId = SubscribeId,
                            Price = subscribe.Price,

                        }
                    }
                };
                await AddOrder(order);
                await SaveChangesAsync();
            }
            else
            {
                var detail = await _context.OrderDetails
                    .FirstOrDefaultAsync(d => d.OrderId == order.Id && d.SubscribeId == SubscribeId);

                if (detail == null)
                {
                    detail = new OrderDetails()
                    {
                        OrderId = order.Id,
                        SubscribeId = SubscribeId,
                        Price = subscribe.Price,
                    };
                    await AddOrderDetails(detail);
                }


                await SaveChangesAsync();
                await UpdatePriceOrder(order.Id);
            }


            return order.Id;
        }

        public async Task<long> AddCourseToOrder(long userId, long courseId)
        {
            var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);

            var course = await _context.Courses.FindAsync(courseId);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinally = false,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetails>()
                    {
                        new OrderDetails()
                        {
                            CourseId = courseId,
                            Price = course.CoursePrice
                        }
                    }
                };
                await AddOrder(order);
                await SaveChangesAsync();
            }
            else
            {
                var detail = await _context.OrderDetails
                    .FirstOrDefaultAsync(d => d.OrderId == order.Id && d.CourseId == courseId);

                if (detail == null)
                {
                    detail = new OrderDetails()
                    {
                        OrderId = order.Id,
                        CourseId = courseId,
                        Price = course.CoursePrice,
                    };
                    await AddOrderDetails(detail);
                }

                await SaveChangesAsync();
                await UpdatePriceOrder(order.Id);
            }


            return order.Id;
        }
        public async Task UpdatePriceOrder(long orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            order.OrderSum = await _context.OrderDetails.Where(d => d.OrderId == orderId).SumAsync(d => d.Price);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task AddOrderDetails(OrderDetails details)
        {
            await _context.OrderDetails.AddAsync(details);
        }
        public async Task UpdateOrder(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderForUserPanel(long userId, long orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Course)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Subscribe)
               .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId);
        }

        public async Task<bool> FinalyOrder(long userId, long orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Course)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null || order.IsFinally)
            {
                return false;
            }

            if (await GetBalanceUserWallet(userId) >= order.OrderSum)
            {
                order.IsFinally = true;
                await AddWalletAsync(new Wallet()
                {
                    Amount = order.OrderSum,
                    PaymentDate = DateTime.Now,
                    IsPay = true,
                    Description = "فاکتور شماره #" + order.Id,
                    UserId = userId,
                    TypeId = 2
                });
                await UpdateOrder(order);

                if (order.OrderDetails.Any(r => r.CourseId != null))
                {
                    var Order = order.OrderDetails.Where(o => o.CourseId != null).ToList();
                    foreach (var detail in Order)
                    {
                        await _context.UserCourses.AddAsync(new UserCourse()
                        {
                            UserId = userId,
                            CourseId = detail.CourseId.Value
                        });
                    }
                }
                if (order.OrderDetails.Any(r => r.SubscribeId != null))
                {
                    var Order = order.OrderDetails.Where(o => o.SubscribeId != null).ToList();
                    foreach (var detail in Order)
                    {
                        await _context.UserSubscribes.AddAsync(new UserSubscribes()
                        {
                            UserId = userId,
                            PaymentDay = DateTime.Now,
                            SubscribeId = detail.SubscribeId.Value
                        });
                    }
                }

                await SaveChangesAsync();
                return true;
            }

            return false;
        }
        #endregion

        #region User
        public async Task<int> GetBalanceUserWallet(long userId)
        {

            var Deposit = await _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToListAsync();

            var Withdraw = await _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToListAsync();

            var Sum = (Deposit.Sum() - Withdraw.Sum());

            return Sum;

        }

        public async Task<long> AddWalletAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();

            return wallet.WalletId;
        }

        public async Task<List<ShowOrderUserPanelViewModel>> GetUserOrder(long userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId)
                .Select(r => new ShowOrderUserPanelViewModel()
                {
                    IsFinally = r.IsFinally,
                    Id = r.Id,
                    OrderSum = r.OrderSum,
                    CreateDate = r.CreateDate
                }).ToListAsync();
        }
        public async Task<Order> GetOrderById(long orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }
        #endregion

        #region Discount
        public async Task<Discount> GetDiscountByCode(string code)
        {
            return await _context.Discounts.SingleOrDefaultAsync(d => d.DiscountCode == code);
        }
        public async Task UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            await SaveChangesAsync();
        }
        public async Task<bool> GetUserDiscountCode(long userId, long DiscountId)
        {
            return await _context.UserDiscountCodes.AnyAsync(d => d.UserId == userId && d.DiscountId == DiscountId);
        }

        public async Task AddUserDiscountCode(UserDiscountCode userDiscountCode)
        {
            await _context.UserDiscountCodes.AddAsync(userDiscountCode);
        }

        public async Task AddDiscount(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
        }

        public async Task<bool> CheckDiscount(string code)
        {
            return await _context.Discounts.AnyAsync(c => c.DiscountCode == code);
        }
        public async Task<FilterDiscountViewModel> GetDiscounts(FilterDiscountViewModel filter)
        {
            var query = _context.Discounts.AsQueryable();

            #region Paging
            await filter.Build(await query.CountAsync()).SetEntities(query);
            #endregion
            return filter;
        }
        public async Task<EditDiscountViewModel> GetDiscountByIdForEdit(long Id)
        {
            return await _context.Discounts
                 .Where(d => d.Id == Id)
                 .Select(r => new EditDiscountViewModel()
                 {
                     Id = r.Id,
                     Code =r.DiscountCode,
                     DiscountPercent =r.DiscountPercent,
                     StartDate =r.StartDate,
                     EndDate=r.EndDate,
                     UsableDiscount =r.UsableCount.Value

                 }).SingleOrDefaultAsync();
        }

        public async Task<Discount> GetDiscountById(long Id)
        {
            return await _context.Discounts.Where(d => d.Id == Id).SingleOrDefaultAsync();
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
