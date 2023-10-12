using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubscriptionService
    {
        private readonly IUnitOfWork unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Subscription>> GetAsyncs() => await unitOfWork.SubscriptionRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Subscription>> FindByConditionAsync(Expression<Func<Subscription, bool>> predicat) => await this.unitOfWork.SubscriptionRepository.FindByConditionAsync(predicat);
        public async Task<Subscription> FindByConditionItemAsync(Expression<Func<Subscription, bool>> predicat) => await this.unitOfWork.SubscriptionRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Subscription subscription) => await unitOfWork.SubscriptionRepository.CreateAsync(subscription);
        public async Task DeleteAsync(int id) => await unitOfWork.SubscriptionRepository.Delete(id);
        public async Task EditAsync(int id, Subscription subscription) => await unitOfWork.SubscriptionRepository.Update(subscription, id);
    }
}
