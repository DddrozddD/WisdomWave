using DAL.Context;
using DAL.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SubQuestionRepository : BaseRepository<SubQuestion>
    {
        public SubQuestionRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var subquestion = await this.Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (subquestion != null)
            {
                this.Entities.Remove(subquestion);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(SubQuestion subquestion, int Id)
        {
            var model = this.Entities.Where(s => s.Id == Id).First();
            model.SubQuestionText = subquestion.SubQuestionText;
            model.Question = subquestion.Question;
            model.QuestionId = subquestion.QuestionId;

            this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails() { IsError = false };
        }
    }
}
