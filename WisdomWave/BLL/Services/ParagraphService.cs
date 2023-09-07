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
    public class ParagraphService
    {
        private readonly IUnitOfWork unitOfWork;

        public ParagraphService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Paragraph>> GetAsyncs() => await unitOfWork.ParagraphRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Paragraph>> FindByConditionAsync(Expression<Func<Paragraph, bool>> predicat) => await this.unitOfWork.ParagraphRepository.FindByConditionAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Paragraph paragraph) => await unitOfWork.ParagraphRepository.CreateAsync(paragraph);
        public async Task DeleteAsync(int id) => await unitOfWork.ParagraphRepository.Delete(id);
        public async Task EditAsync(int id, Paragraph paragraph) => await unitOfWork.ParagraphRepository.Update(paragraph, id);
    }
}
