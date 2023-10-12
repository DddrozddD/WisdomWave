using DAL.Models;
using DAL.Repositories;
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
    public class ReviewService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Review>> GetAsyncs() => await unitOfWork.ReviewRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Review>> FindByConditionAsync(Expression<Func<Review, bool>> predicat) => await this.unitOfWork.ReviewRepository.FindByConditionAsync(predicat);
        public async Task<Review> FindByConditionItemAsync(Expression<Func<Review, bool>> predicat) => await this.unitOfWork.ReviewRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Review review) => await unitOfWork.ReviewRepository.CreateAsync(review);
        public async Task DeleteAsync(int id) => await unitOfWork.ReviewRepository.Delete(id);
        public async Task EditAsync(int id, Review review) => await unitOfWork.ReviewRepository.Update(review, id);
    }
}
