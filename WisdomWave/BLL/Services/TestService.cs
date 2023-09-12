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
    public class TestService
    {
        private readonly IUnitOfWork unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Test>> GetAsyncs() => await unitOfWork.TestRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Test>> FindByConditionAsync(Expression<Func<Test, bool>> predicat) => await this.unitOfWork.TestRepository.FindByConditionAsync(predicat);
        public async Task<Test> FindByConditionItemAsync(Expression<Func<Test, bool>> predicat) => await this.unitOfWork.TestRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Test test) => await unitOfWork.TestRepository.CreateAsync(test);
        public async Task DeleteAsync(int id) => await unitOfWork.TestRepository.Delete(id);
        public async Task EditAsync(int id, Test test) => await unitOfWork.TestRepository.Update(test, id);
    }
}
