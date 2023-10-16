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
    public class SubQuestionServiceDAL
    {
        private readonly IUnitOfWork unitOfWork;

        public SubQuestionServiceDAL(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<SubQuestion>> GetAsyncs() => await unitOfWork.SubQuestionRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<SubQuestion>> FindByConditionAsync(Expression<Func<SubQuestion, bool>> predicat) => await this.unitOfWork.SubQuestionRepository.FindByConditionAsync(predicat);
        public async Task<SubQuestion> FindByConditionItemAsync(Expression<Func<SubQuestion, bool>> predicat) => await this.unitOfWork.SubQuestionRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(SubQuestion subquestion) => await unitOfWork.SubQuestionRepository.CreateAsync(subquestion);
        public async Task DeleteAsync(int id) => await unitOfWork.SubQuestionRepository.Delete(id);
        public async Task EditAsync(int id, SubQuestion subquestion) => await unitOfWork.SubQuestionRepository.Update(subquestion, id);
    }
}
