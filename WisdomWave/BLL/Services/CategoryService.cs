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
        public async Task<OperationDetails> CreateAsync(Category category)
        {

          return await unitOfWork.CategoryRepository.CreateAsync(category);
        }

       

        public async Task<OperationDetails> CreateAsync(Category category, int categoryParentId)
        {
            var res =  await unitOfWork.CategoryRepository.CreateAsync(category);
            Category parentCategory = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.Id == categoryParentId);
            Category newCategory = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.CategoryName == category.CategoryName);
            List<Category> childCategories = new List<Category>();
            if (parentCategory.ChildCategories != null) {
                childCategories = parentCategory.ChildCategories.ToList();
            }
            childCategories.Add(newCategory);
            parentCategory.ChildCategories = childCategories;
            await unitOfWork.CategoryRepository.Update(parentCategory, parentCategory.Id);
            return res;
        }

       
        public async Task DeleteAsync(int id) => await unitOfWork.CategoryRepository.Delete(id);
        public async Task<OperationDetails> EditAsync(int id, Category category) => await unitOfWork.CategoryRepository.Update(category, id);


        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithParentChild()
        {

            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => (c.ParentCategories.Count() != 0)&&(c.ChildCategories.Count() != 0));
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutParentAsync()
        {

            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Count() == 0);
        }
        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutChildAsync()
        {
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ChildCategories.Count() == 0);
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByParentNameAsync(string categoryName)
        {

            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Any(pc => pc.CategoryName == categoryName));

        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByChildNameAsync(string categoryName)
        {

            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ChildCategories.Any(pc => pc.CategoryName == categoryName));

        }
    }
}
