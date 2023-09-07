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
    public class ParagraphRepository : BaseRepository<Paragraph>
    {
        public ParagraphRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var paragraph = await this.Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (paragraph != null)
            {
                this.Entities.Remove(paragraph);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Paragraph paragraph, int Id)
        {
            var model = this.Entities.Where(s => s.Id == Id).First();
            model.ParagraphText = paragraph.ParagraphText;
            model.PhotoLinks = paragraph.PhotoLinks;
            model.ParagraphName = paragraph.ParagraphName;
            model.video_links = paragraph.video_links;
            model.UnitsID = paragraph.UnitsID;
            model.Unit = paragraph.Unit;

            this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails() { IsError = false };
        }
    }
}
