using DAL.Context;
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
    public class UnitRepository : BaseRepository<Unit>
    {
        public UnitRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var unit = await this.Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (unit != null)
            {
                this.Entities.Remove(unit);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Unit unit, int Id)
        {
            try { 
            var model = this.Entities.Where(s => s.Id == Id).First();
            model.Number = unit.Number;
            model.UnitName = unit.UnitName;
            model.DateOfCreate = unit.DateOfCreate;
            model.courseId = unit.courseId;
            model.Course = unit.Course;
            model.PassedUnitUsers = unit.PassedUnitUsers;
            model.Paragraphs = unit.Paragraphs;
            model.Tests = unit.Tests;

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
