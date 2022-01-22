using Academy.Domain.Entities.Subscribe;
using Academy.Domain.ViewModels.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
   public interface ISubscribeService
    {
        #region Subscribe
        Task<List<Subscribe>> GetSubscribes();
        Task<Subscribe> GetSubscribeById(long Id);
        Task CreateSubscribe(Subscribe subscribe);
        Task EditSubscribe(Subscribe subscribe);
        Task<List<ShowSubscribeViewModel>> GetSubscribeForShow();
        Task<int> SubscribeCount();
        Task<List<ShowSubscribeBuyersViewModel>> GetSubscribeBuyers(long subscribeId);

        #endregion
    }
}
