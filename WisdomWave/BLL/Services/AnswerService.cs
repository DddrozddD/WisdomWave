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
    public class AnswerService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnswerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Answer>> GetAsyncs() => await unitOfWork.AnswerRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Answer>> FindByConditionAsync(Expression<Func<Answer, bool>> predicat) => await this.unitOfWork.AnswerRepository.FindByConditionAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Answer answer) => await unitOfWork.AnswerRepository.CreateAsync(answer);
        public async Task DeleteAsync(int id) => await unitOfWork.AnswerRepository.Delete(id);
        public async Task EditAsync(int id, Answer answer) => await unitOfWork.AnswerRepository.Update(answer, id);
    }
}
