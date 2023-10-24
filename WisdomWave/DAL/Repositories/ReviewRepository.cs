using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReviewRepository : BaseRepository<Review>
    {
        public ReviewRepository(WwContext context) : base(context) { }

        public async Task Delete(int id)
        {
            var review = await this.Entities.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
            if (review != null)
            {
                this.Entities.Remove(review);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<OperationDetails> Update(Review review, int Id)
        {
            try { 
            var model = this.Entities.Where(m => m.Id == Id).First();
            model.TextReview = review.TextReview;
            model.User = review.User;
            model.LikesDislikes = review.LikesDislikes;
            model.Course = review.Course;
            model.courseId = review.courseId;
            model.userId = review.userId;

            this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails { Message = "Created" };
        }
            catch (Exception ex)
            {
                return new OperationDetails { Message = "Create Fatal Error", exception = ex, IsError = true };
}
        }
    }
}