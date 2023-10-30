using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        private readonly WwContext _context;

        public CategoryRepository(WwContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.ChildCategories).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyCollection<Category>> FindByConditionAsync(Expression<Func<Category, bool>> predicat)
        {
            return await _context.Categories
                .Include(c => c.ChildCategories)
                .Where(predicat)
                .ToListAsync();
        }

        public async Task<Category> FindByConditionItemAsync(Expression<Func<Category, bool>> predicat)
        {
            return await _context.Categories
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(predicat);
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Category> FindCategoryByIdAsync(int categoryId, List<Category> categoryList)
        {
            return categoryList.FirstOrDefault(c => c.Id == categoryId);
        }

    }
}
