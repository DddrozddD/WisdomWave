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
    public class RatingService
    {
     
            private readonly IUnitOfWork unitOfWork;

            public RatingService(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<IReadOnlyCollection<Rating>> GetAsyncs() => await unitOfWork.RatingRepository.GetAllAsync();
            public async Task<IReadOnlyCollection<Rating>> FindByConditionAsync(Expression<Func<Rating, bool>> predicat) => await this.unitOfWork.RatingRepository.FindByConditionAsync(predicat);
            public async Task<Rating> FindByConditionItemAsync(Expression<Func<Rating, bool>> predicat) => await this.unitOfWork.RatingRepository.FindByConditionItemAsync(predicat);


            public async Task<OperationDetails> CreateAsync(Rating rating)
            {
                OperationDetails result = await unitOfWork.RatingRepository.CreateAsync(rating);

                return result;
            }

         

            public async Task DeleteAsync(int courseId, string userId) => await unitOfWork.RatingRepository.Delete(courseId, userId);
            public async Task<OperationDetails> EditAsync(int courseId, string userId, Rating rating) => await unitOfWork.RatingRepository.Update(rating, courseId, userId);
        }
    
}
