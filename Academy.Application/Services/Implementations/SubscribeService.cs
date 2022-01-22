using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities.Subscribe;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Implementations
{
    public class SubscribeService : ISubscribeService
    {
        #region constructor
        private readonly ISubscribeRepository _subscribeRepository;
        public SubscribeService(ISubscribeRepository subscribeRepository)
        {
            _subscribeRepository = subscribeRepository;
        }


        #endregion

        #region Subscribe
        public async Task<Subscribe> GetSubscribeById(long Id)
        {
            return await _subscribeRepository.GetSubscribeById(Id);
        }

        public async Task<List<Subscribe>> GetSubscribes()
        {
            return await _subscribeRepository.GetSubscribes();
        }
        public async Task CreateSubscribe(Subscribe subscribe)
        {
            var newSubscribe = new Subscribe()
            {
                Title = subscribe.Title,
                Price = subscribe.Price,
                Description = subscribe.Description
            };
            await _subscribeRepository.AddSubscribe(subscribe);
            await _subscribeRepository.SaveChangesAsync();
        }

        public async Task EditSubscribe(Subscribe subscribe)
        {
            var oldSubscribe = await _subscribeRepository.GetSubscribeById(subscribe.Id);

            //update 
            oldSubscribe.Title = subscribe.Title;
            oldSubscribe.Description = subscribe.Description;
            oldSubscribe.Price = subscribe.Price;

            await _subscribeRepository.UpdateSubscribe(oldSubscribe);
           
            
        }
        public async Task<List<ShowSubscribeViewModel>> GetSubscribeForShow()
        {
            return await _subscribeRepository.GetSubscribeForShow();
        }

        public async Task<int> SubscribeCount()
        {
            return await _subscribeRepository.SubscribeCount();
        }

        public async Task<List<ShowSubscribeBuyersViewModel>> GetSubscribeBuyers(long subscribeId)
        {
            return await _subscribeRepository.GetSubscribeBuyers(subscribeId);
        }

        #endregion
    }
}
