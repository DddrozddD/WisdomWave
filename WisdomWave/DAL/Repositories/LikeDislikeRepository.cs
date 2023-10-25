using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LikeDislikeRepository : BaseRepository<LikeDislike>
    {
        public LikeDislikeRepository(WwContext context) : base(context) { }

        public async Task Delete(string userId, int reviewId)
        {
            var likeDislike = await this.Entities.FirstOrDefaultAsync(m => (m.userId == userId) && (m.reviewId == reviewId)).ConfigureAwait(false);
            if (likeDislike != null)
            {
                this.Entities.Remove(likeDislike);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<OperationDetails> Update(LikeDislike likeDislike, string userId, int reviewId)
        {
            try
            {
                var model = this.Entities.Where(m => (m.userId == userId)&&(m.reviewId == reviewId)).First();
            model.Review = likeDislike.Review;
            model.reviewId = likeDislike.reviewId;
            model.userId = likeDislike.userId;
            model.IsLike = likeDislike.IsLike;

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
