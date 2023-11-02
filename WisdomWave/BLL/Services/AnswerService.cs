using DAL.Models;
using DAL.Repositories;
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
    public class AnswerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AnswerRepository answerRepository;
        private readonly QuestionRepository questionRepository;

        public AnswerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Answer>> GetAsyncs() => await unitOfWork.AnswerRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Answer>> FindByConditionAsync(Expression<Func<Answer, bool>> predicat) => await this.unitOfWork.AnswerRepository.FindByConditionAsync(predicat);
        public async Task<Answer> FindByConditionItemAsync(Expression<Func<Answer, bool>> predicat) => await this.unitOfWork.AnswerRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Answer answer,int tpQuestionId){

            Question question = await unitOfWork.QuestionRepository.FindByConditionItemAsync(q => q.Id == tpQuestionId);
            SubQuestion subQuestion = await unitOfWork.SubQuestionRepository.FindByConditionItemAsync(sq => sq.Id == tpQuestionId);


            answer.Question = question;
            answer.questionId = tpQuestionId;

            answer.SubQuestion = subQuestion;
            answer.subQuestionId = tpQuestionId;

            OperationDetails result = await unitOfWork.AnswerRepository.CreateAsync(answer);

            if (result.IsError == false)
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
            }

            return result;
        }
        public async Task DeleteAsync(int id) => await unitOfWork.AnswerRepository.Delete(id);
        public async Task<OperationDetails> EditAsync(int id, Answer answer) => await unitOfWork.AnswerRepository.Update(answer, id);
    }
}
