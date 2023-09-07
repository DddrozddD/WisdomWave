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
            var model = this.Entities.Where(s => s.Id == Id).First();
            model.number = unit.number;
            model.UnitName = unit.UnitName;
            model.DateOfCreate = unit.DateOfCreate;
            model.CourseId = unit.CourseId;
            model.Course = unit.Course;

            this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails() { IsError = false };

        }
