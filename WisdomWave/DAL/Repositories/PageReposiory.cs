using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PageRepository : BaseRepository<Page>
    {
        public PageRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var page = await this.Entities.FirstOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);
            if (page != null)
            {
                this.Entities.Remove(page);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Page page, int Id)
        {
            try
            {
                var model = this.Entities.Where(s => s.Id == Id).First();
                model.PageName = page.PageName;
                model.PhotoLinks = page.PhotoLinks;
                model.VideoLinks = page.VideoLinks;
                model.unitId = page.unitId;
                model.Unit = page.Unit;
                model.PassedPageUsers = page.PassedPageUsers;
                model.Paragraphs = page.Paragraphs;



                this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();

                return new OperationDetails { Message = "Updated" };
            }
            catch (Exception ex)
            {
                return new OperationDetails { Message = "Create Fatal Error", exception = ex, IsError = true };
            }
        }
    }
}
