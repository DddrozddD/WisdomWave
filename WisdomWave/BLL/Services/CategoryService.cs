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
            Category parentCategory = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.Id == categoryParentId);
            List<Category> parentCategories = new List<Category>();
            parentCategories.Add(parentCategory);

            category.ParentCategories = parentCategories;
            return await unitOfWork.CategoryRepository.CreateAsync(category);
        }

        public async Task<OperationDetails> CreateAsync(Category category, int courseId, int categoryParentId)
        {

            Course course = await unitOfWork.CourseRepository.FindByConditionItemAsync(c => c.Id == courseId);

            List<Course> courses = new List<Course>();
            courses.Add(course);
            category.Courses = courses;

            Category parentCategory = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c=>c.Id==categoryParentId);
            List<Category> parentCategories = new List<Category>();
            parentCategories.Add(parentCategory);

            return await unitOfWork.CategoryRepository.CreateAsync(category);
        }
        public async Task DeleteAsync(int id) => await unitOfWork.CategoryRepository.Delete(id);
        public async Task<OperationDetails> EditAsync(int id, Category category) => await unitOfWork.CategoryRepository.Update(category, id);

        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutParentAsync()
        {
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories == null);
        }
        public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutChildAsync()
        {
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ChildCategories == null);
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByParentIdAsync(int parentId)
        {

            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Any(pc => pc.Id == parentId));

        }

        public async Task<IReadOnlyCollection<Category>> GetParentCategoriesByIdAsync(int childId)
        {


            var category = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.Id == childId);

            return category.ParentCategories;
        }

        public async Task<IReadOnlyCollection<Category>> GetChildCategoriesByIdAsync(int categoryId)
        {

            var category = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.Id == categoryId);

            return category.ChildCategories;
        }
    }
}
