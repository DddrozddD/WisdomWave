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
    public class QuestionRepository : BaseRepository<Question>
    {
        public QuestionRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var question = await Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (question != null)
            {
                Entities.Remove(question);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Question question, int Id)
        {
            var model = Entities.Where(s => s.Id == Id).First();
            model.QuestionName = question.QuestionName;
            model.QuestionText = question.QuestionText;
            model.QuestionType = question.QuestionType;
            model.TestId = question.TestId;
            model.Test = question.Test;

            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails() { IsError = false };
        }
    }
}
