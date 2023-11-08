using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class QuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Question>> GetAsyncs() => await unitOfWork.QuestionRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Question>> FindByConditionAsync(Expression<Func<Question, bool>> predicat) => await this.unitOfWork.QuestionRepository.FindByConditionAsync(predicat);
        public async Task<Question> FindByConditionItemAsync(Expression<Func<Question, bool>> predicat) => await this.unitOfWork.QuestionRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Question question, int testId)
        {
            Test test = await unitOfWork.TestRepository.FindByConditionItemAsync(t => t.Id == testId);

            question.Test = test;
            question.testId = testId;

            OperationDetails result = await unitOfWork.QuestionRepository.CreateAsync(question);

            /*if (result.IsError == false)
            {
                Question newQuestion = await unitOfWork.QuestionRepository.FindByConditionItemAsync(q => (q.Id == question.Id) && (q.Test == question.Test) && (q.QuestionType == question.QuestionType));

                test.Questions.ToList().Add(newQuestion);

                IReadOnlyCollection<Question> newQuestions = new ReadOnlyCollection<Question>(test.Questions.ToList());
                test.Questions = newQuestions;
                await unitOfWork.TestRepository.Update(test, testId);
            }*/

            return result;
        }
    
        public async Task DeleteAsync(int id) => await unitOfWork.QuestionRepository.Delete(id);
        public async Task EditAsync(int id, Question question) => await unitOfWork.QuestionRepository.Update(question, id);
    }
}
