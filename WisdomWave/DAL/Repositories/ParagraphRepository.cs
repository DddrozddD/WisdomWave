﻿using DAL.Context;
using DAL.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
            try { 
            var model = this.Entities.Where(s => s.Id == Id).First();
            model.ParagraphText = paragraph.ParagraphText;
                model.ParagraphName = paragraph.ParagraphName;
                model.Page = paragraph.Page;
            model.pageId= paragraph.pageId;


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
