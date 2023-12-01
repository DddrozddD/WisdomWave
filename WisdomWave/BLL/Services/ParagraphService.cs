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
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IReadOnlyCollection<Paragraph>> FindByConditionAsync(Expression<Func<Paragraph, bool>> predicat) => await unitOfWork.ParagraphRepository.FindByConditionAsync(predicat);
        public async Task<Paragraph> FindByConditionItemAsync(Expression<Func<Paragraph, bool>> predicat) => await unitOfWork.ParagraphRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Paragraph paragraph)
        {
          

            OperationDetails result = await unitOfWork.ParagraphRepository.CreateAsync(paragraph);
          

            return result;
        }
        public async Task DeleteAsync(int id) => await unitOfWork.ParagraphRepository.Delete(id);
        public async Task EditAsync(int id, Paragraph paragraph) => await unitOfWork.ParagraphRepository.Update(paragraph, id);
    }
}
