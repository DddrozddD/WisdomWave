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
    public class CourseRepository : BaseRepository<Course>
    {
        public CourseRepository(WwContext context) : base(context) { }

        public async Task Delete(int id)
        {
            var course = await this.Entities.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
            if (course != null)
            {
                this.Entities.Remove(course);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<OperationDetails> Update(Course course, int Id)
        {
            try
            {
                var model = this.Entities.Where(m => m.Id == Id).First();
                model.CourseName = course.CourseName;
                model.creatorUserId = course.creatorUserId;
                // model.LearnerUsers = course.LearnerUsers;
                model.CourseName = course.CourseName;
                model.ImageLinkCourse = course.ImageLinkCourse;
                model.RatingCourse = course.RatingCourse;
                model.DateOfCreate = course.DateOfCreate;
                model.Description = course.Description;
                model.Reviews = course.Reviews;

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