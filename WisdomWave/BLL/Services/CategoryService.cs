using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Repositories;
using Domain.Models;

namespace BLL.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyCollection<Category>> GetAsyncs() => await _categoryRepository.GetAllAsync();
        public async Task<Category> GetByIdAsync(int id) => await _categoryRepository.GetByIdAsync(id);
        public async Task<IReadOnlyCollection<Category>> FindByConditionAsync(Expression<Func<Category, bool>> predicat) => await _categoryRepository.FindByConditionAsync(predicat);
        public async Task<Category> FindByConditionItemAsync(Expression<Func<Category, bool>> predicat) => await _categoryRepository.FindByConditionItemAsync(predicat);
        public async Task CreateAsync(Category category) => await _categoryRepository.CreateAsync(category);
        public async Task UpdateAsync(int id, Category category) => await _categoryRepository.UpdateAsync(category);
        public async Task DeleteAsync(int id) => await _categoryRepository.DeleteAsync(id);
        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutParentAsync()
        {
            return await _categoryRepository.FindByConditionAsync(c => c.ParentCategories.Count == 0);
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByParentIdAsync(int parentId)
        {
            return await _categoryRepository.FindByConditionAsync(c => c.ParentCategories.Any(pc => pc.Id == parentId));
        }

        public async Task<IReadOnlyCollection<Category>> GetParentCategoriesByIdAsync(int categoryId)
        {
            var parentCategories = new List<Category>();
            var category = await _categoryRepository.FindByConditionItemAsync(c => c.Id == categoryId);

            while (category != null)
            {
                parentCategories.Add(category);
                if (category.ParentCategories.Count == 0)
                {
                    break;
                }
                category = await _categoryRepository.FindByConditionItemAsync(c => c.Id == category.ParentCategories.First().Id);
            }

            return parentCategories;
        }
        public async Task<Category> FindCategoryByIdAsync(int categoryId, List<Category> categoryList)
        {
            return categoryList.FirstOrDefault(c => c.Id == categoryId);
        }


    }
}
