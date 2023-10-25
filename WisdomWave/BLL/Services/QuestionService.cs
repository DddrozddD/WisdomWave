using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
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
        public async Task<OperationDetails> CreateAsync(Question question) => await unitOfWork.QuestionRepository.CreateAsync(question);
        public async Task DeleteAsync(int id) => await unitOfWork.QuestionRepository.Delete(id);
        public async Task EditAsync(int id, Question question) => await unitOfWork.QuestionRepository.Update(question, id);
    }
}
