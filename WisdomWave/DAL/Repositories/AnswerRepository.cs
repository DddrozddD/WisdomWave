using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>
    {
        public AnswerRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var answer = await this.Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (answer != null)
            {
                this.Entities.Remove(answer);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Answer answer, int Id)
        {
            try
            {


                var model = this.Entities.Where(s => s.Id == Id).First();
                model.IsCorrect = answer.IsCorrect;
                model.AnswerText = answer.AnswerText;
                model.subQuestionId = answer.subQuestionId;
                model.questionId = answer.questionId;
                model.SubQuestion = answer.SubQuestion;
                model.Question = answer.Question;

                this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();

                return new OperationDetails { Message = "Created" };
            }
            catch (Exception ex)
            {
                return new OperationDetails { Message = "Create Fatal Error", exception = ex, IsError = true };
            }
            }
    }
}
