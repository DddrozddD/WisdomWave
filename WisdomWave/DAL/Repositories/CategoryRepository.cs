using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
<<<<<<< Updated upstream

        public CategoryRepository(WwContext context) : base(context) { }

        public async Task Delete(int id)
        {
            var category = await this.Entities.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
            if (category != null)
            {
                this.Entities.Remove(category);
=======
        public CategoryRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var answer = await this.Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (answer != null)
            {
                this.Entities.Remove(answer);
>>>>>>> Stashed changes
            }
            await _context.SaveChangesAsync();

        }
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        public async Task<OperationDetails> Update(Category category, int Id)
        {
            try
            {
<<<<<<< Updated upstream
                var model = this.Entities.Where(m => m.Id == Id).First();
=======


                var model = this.Entities.Where(s => s.Id == Id).First();
>>>>>>> Stashed changes
                model.CategoryName = category.CategoryName;
                model.ParentCategories = category.ParentCategories;
                model.ChildCategories = category.ChildCategories;
                model.Courses = category.Courses;
<<<<<<< Updated upstream
                
=======

>>>>>>> Stashed changes
                this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();

                return new OperationDetails { Message = "Created" };
            }
            catch (Exception ex)
            {
                return new OperationDetails { Message = "Create Fatal Error", exception = ex, IsError = true };
            }
        }
<<<<<<< Updated upstream


=======
>>>>>>> Stashed changes
    }
}
