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
    public class LearnerUserToCourseRepository : BaseRepository<LearnerUserToCourse>
    {
        public LearnerUserToCourseRepository(WwContext context) : base(context) { }

        public async Task Delete(string userId,int courseId)
        {
            var model = await this.Entities.FirstOrDefaultAsync(m => (m.userId == userId)&&(m.courseId==courseId)).ConfigureAwait(false);
            if (model != null)
            {
                this.Entities.Remove(model);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<OperationDetails> Update(LearnerUserToCourse lUserToCourse, string userId, int courseId)
        {
            var model = this.Entities.Where(m => (m.userId == userId) && (m.courseId == courseId)).First();
            model.User = lUserToCourse.User;
            model.userId = lUserToCourse.userId;
            model.courseId = lUserToCourse.courseId;
            model.Course = lUserToCourse.Course;

            this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails() { IsError = false };
        }
    }
}
