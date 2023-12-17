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
    public class RatingRepository : BaseRepository<Rating>
    {
        public RatingRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int courseId, string userId)
        {
            var rating = await this.Entities.FirstOrDefaultAsync(r => (r.courseId == courseId)&&(r.userId==userId)).ConfigureAwait(false);
            if (rating != null)
            {
                this.Entities.Remove(rating);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Rating rating, int courseId, string userId)
        {
            try
            {
                var model = await this.Entities.FirstOrDefaultAsync(r => (r.courseId == courseId) && (r.userId == userId)).ConfigureAwait(false);
               model.Course = rating.Course;
                model.courseId = rating.courseId;
                model.userId = rating.userId;
                model.User = rating.User;

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
