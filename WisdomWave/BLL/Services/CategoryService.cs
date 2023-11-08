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
<<<<<<< Updated upstream
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
=======
        public async Task<OperationDetails> CreateAsync(Category category, int courseId)
        {

            Course course = await unitOfWork.CourseRepository.FindByConditionItemAsync(c=>c.Id== courseId);

            List<Course> courses = new List<Course>();
            courses.Add(course);
            category.Courses = courses;

            /* if (result.IsError == false)
             {
                 if(question != null && subQuestion == null)
                 {
                     Answer newAnswer = await answerRepository.FindByConditionItemAsync(a => (a.Id == answer.Id) && (a.AnswerText == a.AnswerText) && (a.IsCorrect == a.IsCorrect) );
                     question.Answers.ToList().Add(newAnswer);
                     IReadOnlyCollection<Answer> newAnswers = new ReadOnlyCollection<Answer>(question.Answers.ToList());
                     question.Answers = newAnswers;
                     await unitOfWork.QuestionRepository.Update(question, tpQuestionId);
                 }
                 if (question != null && subQuestion != null)
                 {
                     Answer newAnswer = await answerRepository.FindByConditionItemAsync(a => (a.Id == answer.Id) && (a.AnswerText == a.AnswerText) && (a.IsCorrect == a.IsCorrect));
                     subQuestion.Answers.ToList().Add(newAnswer);
                     IReadOnlyCollection<Answer> newAnswers = new ReadOnlyCollection<Answer>(subQuestion.Answers.ToList());
                     subQuestion.Answers = newAnswers;
                     await unitOfWork.SubQuestionRepository.Update(subQuestion, tpQuestionId);
                 }
             }*/

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
>>>>>>> Stashed changes
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByParentIdAsync(int parentId)
        {
<<<<<<< Updated upstream
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Any(c=>c.Id==parentId));
=======
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ParentCategories.Any(pc => pc.Id == parentId));
>>>>>>> Stashed changes
        }

        public async Task<IReadOnlyCollection<Category>> GetParentCategoriesByIdAsync(int childId)
        {
<<<<<<< Updated upstream
            return await unitOfWork.CategoryRepository.FindByConditionAsync(c => c.ChildCategories.Any(c => c.Id == childId));
=======

            var category = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.Id == categoryId);

            return category.ParentCategories;
>>>>>>> Stashed changes
        }

        public async Task<IReadOnlyCollection<Category>> GetChildCategoriesByIdAsync(int categoryId)
        {
            return categoryList.FirstOrDefault(c => c.Id == categoryId);
        }

            var category = await unitOfWork.CategoryRepository.FindByConditionItemAsync(c => c.Id == categoryId);

            return category.ChildCategories;
        }
    }
}
