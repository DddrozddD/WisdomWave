using DAL.Models;
using DAL.Repositories;
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
        public async Task<OperationDetails> CreateAsync(Review review, int courseId)
        {
            Course course = await unitOfWork.CourseRepository.FindByConditionItemAsync(c => c.Id == courseId);

            review.Course = course;
            review.courseId = courseId;

            OperationDetails result = await unitOfWork.ReviewRepository.CreateAsync(review);

            if (result.IsError == false)
            {
                Review newReview = await unitOfWork.ReviewRepository.FindByConditionItemAsync(r => (r.Id == review.Id) && (r.courseId == review.courseId) && (r.Course == review.Course) && (r.TextReview == review.TextReview));

                course.Reviews.ToList().Add(newReview);

                IReadOnlyCollection<Review> newReviews = new ReadOnlyCollection<Review>(course.Reviews.ToList());
                course.Reviews = newReviews;
                await unitOfWork.CourseRepository.Update(course, courseId);
            }

            return result;
        }
        public async Task DeleteAsync(int id) => await unitOfWork.ReviewRepository.Delete(id);
        public async Task EditAsync(int id, Review review) => await unitOfWork.ReviewRepository.Update(review, id);
    }
}
