using Academy.Data.Context;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Subscribe;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Subscribes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Data.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;
        public SubscribeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region subscribe
        public async Task<List<Subscribe>> GetSubscribes()
        {
            return await _context.Subscribes.ToListAsync();
        }
        public async Task<Subscribe> GetSubscribeById(long Id)
        {
            return await _context.Subscribes.Where(s => s.Id == Id).SingleOrDefaultAsync();
        }


        public async Task AddSubscribe(Subscribe subscribe)
        {
            await _context.Subscribes.AddAsync(subscribe);
        }
        public async Task UpdateSubscribe(Subscribe subscribe)
        {
            _context.Subscribes.Update(subscribe);
            await SaveChangesAsync();
        }
        public async Task<List<ShowSubscribeViewModel>> GetSubscribeForShow()
        {
            return await _context.Subscribes
                .Select(r => new ShowSubscribeViewModel()
                {
                    Title = r.Title,
                    Description =r.Description,
                    Price = r.Price,
                    Id = r.Id
                })
                .Take(3)
                .ToListAsync();
        }

        public async Task<int> SubscribeCount()
        {
            return await _context.UserSubscribes.CountAsync();
        }

        public async Task<List<ShowSubscribeBuyersViewModel>> GetSubscribeBuyers(long subscribeId)
        {
            return await _context.UserSubscribes.Include(u=> u.User)
                .Where(r=>r.SubscribeId ==subscribeId)
                .Select(r=> new ShowSubscribeBuyersViewModel()
                {
                    SubscribeId = r.SubscribeId,
                    UserId = r.UserId,
                    UserName = r.User.UserName,
                    Email = r.User.Email,
                    PaymentDay =r.PaymentDay,
                    PhoneNumber =r.User.PhoneNumber
                    
                })
                .OrderByDescending(p=>p.PaymentDay)
                .ToListAsync();
        }
        #endregion



        #region Save 
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
