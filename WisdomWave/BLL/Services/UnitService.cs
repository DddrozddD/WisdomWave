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
    public class UnitService
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Unit>> GetAsyncs() => await unitOfWork.UnitRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Unit>> FindByConditionAsync(Expression<Func<Unit, bool>> predicat) => await this.unitOfWork.UnitRepository.FindByConditionAsync(predicat);
        public async Task<Unit> FindByConditionItemAsync(Expression<Func<Unit, bool>> predicat) => await this.unitOfWork.UnitRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Unit unit) => await unitOfWork.UnitRepository.CreateAsync(unit);
        public async Task DeleteAsync(int id) => await unitOfWork.UnitRepository.Delete(id);
        public async Task EditAsync(int id, Unit unit) => await unitOfWork.UnitRepository.Update(unit, id);
    }
}
