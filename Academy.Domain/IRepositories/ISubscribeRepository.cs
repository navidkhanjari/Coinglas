using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Subscribe;
using Academy.Domain.ViewModels.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.IRepositories
{
    public interface ISubscribeRepository
    {
        #region Subscribes
        Task<List<Subscribe>> GetSubscribes();
        Task<Subscribe> GetSubscribeById(long Id);
        Task AddSubscribe(Subscribe subscribe);
        Task UpdateSubscribe(Subscribe subscribe);
        Task<List<ShowSubscribeViewModel>> GetSubscribeForShow();
        Task<int> SubscribeCount();
        Task<List<ShowSubscribeBuyersViewModel>> GetSubscribeBuyers(long subscribeId);
        #endregion

       

        #region Save
        Task SaveChangesAsync();
        #endregion
    }
}
