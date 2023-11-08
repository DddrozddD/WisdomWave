using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LikeDislikeService
    {
        private readonly IUnitOfWork unitOfWork;

        public LikeDislikeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<LikeDislike>> GetAsyncs() => await unitOfWork.LikeDislikeRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<LikeDislike>> FindByConditionAsync(Expression<Func<LikeDislike, bool>> predicat) => await this.unitOfWork.LikeDislikeRepository.FindByConditionAsync(predicat);
        public async Task<LikeDislike> FindByConditionItemAsync(Expression<Func<LikeDislike, bool>> predicat) => await this.unitOfWork.LikeDislikeRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(LikeDislike likeDislike, int reviewId)
        {
            Review review = await unitOfWork.ReviewRepository.FindByConditionItemAsync(r => r.Id == likeDislike.reviewId);

            likeDislike.Review = review;
            likeDislike.reviewId = reviewId;

            OperationDetails result = await unitOfWork.LikeDislikeRepository.CreateAsync(likeDislike);

            /*if (result.IsError == false)
            {
                LikeDislike newlikeDislike = await unitOfWork.LikeDislikeRepository.FindByConditionItemAsync(lk => (lk.IsLike == likeDislike.IsLike) && (lk.Review == likeDislike.Review) && (lk.reviewId == likeDislike.reviewId));

                review.LikesDislikes.ToList().Add(newlikeDislike);

                IReadOnlyCollection<LikeDislike> newlikeDislikes = new ReadOnlyCollection<LikeDislike>(review.LikesDislikes.ToList());
                review.LikesDislikes = newlikeDislikes;
                await unitOfWork.ReviewRepository.Update(review, reviewId);
            }*/

            return result;
        }
        public async Task DeleteAsync(string userId, int reviewId) => await unitOfWork.LikeDislikeRepository.Delete(userId, reviewId);
        public async Task EditAsync(string userId, int reviewId, LikeDislike likeDislike) => await unitOfWork.LikeDislikeRepository.Update(likeDislike, userId, reviewId);
    }
}
