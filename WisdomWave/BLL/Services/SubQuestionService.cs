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
    public class SubQuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        public SubQuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<SubQuestion>> GetAsyncs() => await unitOfWork.SubQuestionRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<SubQuestion>> FindByConditionAsync(Expression<Func<SubQuestion, bool>> predicat) => await this.unitOfWork.SubQuestionRepository.FindByConditionAsync(predicat);
        public async Task<SubQuestion> FindByConditionItemAsync(Expression<Func<SubQuestion, bool>> predicat) => await this.unitOfWork.SubQuestionRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(SubQuestion subquestion, int questionId){
           
            Question question = await unitOfWork.QuestionRepository.FindByConditionItemAsync(c => c.Id == questionId);

            subquestion.Question = question;
            subquestion.questionId = question.Id;

            OperationDetails result = await unitOfWork.SubQuestionRepository.CreateAsync(subquestion);

            /*if (result.IsError == false)
            {
                SubQuestion subQuestion = await unitOfWork.SubQuestionRepository.FindByConditionItemAsync(sq => (sq.Id == subquestion.Id) && (sq.Question == subquestion.Question) && (sq.questionId == subquestion.questionId));

                question.SubQuestions.ToList().Add(subQuestion);

                IReadOnlyCollection<SubQuestion> newSubQuestions = new ReadOnlyCollection<SubQuestion>(question.SubQuestions.ToList());
                question.SubQuestions= newSubQuestions;
                await unitOfWork.QuestionRepository.Update(question, questionId);
            }*/

            return result;
        }
        public async Task DeleteAsync(int id) => await unitOfWork.SubQuestionRepository.Delete(id);
        public async Task EditAsync(int id, SubQuestion subquestion) => await unitOfWork.SubQuestionRepository.Update(subquestion, id);
    }
}
