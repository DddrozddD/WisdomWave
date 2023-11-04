using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Category>> GetAsyncs() => await unitOfWork.CategoryRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Category>> FindByConditionAsync(Expression<Func<Category, bool>> predicat) => await this.unitOfWork.CategoryRepository.FindByConditionAsync(predicat);
        public async Task<Category> FindByConditionItemAsync(Expression<Func<Category, bool>> predicat) => await this.unitOfWork.CategoryRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Category category, int parentId) {
            Category parentCategory = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c=>c.Id==parentId);

            List<Category> parentList = new List<Category>();
           
                parentList.Add(parentCategory);
                category.ParentCategories = parentList;
            
            OperationDetails result = await unitOfWork.CategoryRepository.CreateAsync(category);
            if (result.IsError == false)
            {
                List<Category> childList;
                if (parentCategory.ChildCategories.ToList() == null)
                {
                    childList = new List<Category>();
                    childList.Add(category);
                    parentCategory.ChildCategories = childList;
                    await unitOfWork.CategoryRepository.Update(category, parentCategory.Id);
                }
                else
                {
                    childList = new List<Category>();
                    childList.Add(category);
                    parentCategory.ChildCategories = childList;
                    await unitOfWork.CategoryRepository.Update(category, parentCategory.Id);
                }
                
            }

            return result;

        }
        public async Task<OperationDetails> CreateAsync(Category category)=> await unitOfWork.CategoryRepository.CreateAsync(category);
        
        public async Task DeleteAsync(int id) => await unitOfWork.CategoryRepository.Delete(id);
        public async Task EditAsync(int id, Category category) => await unitOfWork.CategoryRepository.Update(category, id);
        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutParentAsync()
        {
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Count == 0);
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByParentIdAsync(int parentId)
        {
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Any(c=>c.Id==parentId));
        }

        public async Task<IReadOnlyCollection<Category>> GetParentCategoriesByIdAsync(int childId)
        {
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ChildCategories.Any(c => c.Id == childId));
        }
        public async Task<Category> FindCategoryByIdAsync(int categoryId, List<Category> categoryList)
        {
            return categoryList.FirstOrDefault(c => c.Id == categoryId);
        }


    }
}
