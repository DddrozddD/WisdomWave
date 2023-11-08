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
    public class TestRepository : BaseRepository<Test>
    {
        public TestRepository(WwContext context) : base(context)
        {
        }

        public async Task Delete(int id)
        {
            var test = await this.Entities.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            if (test != null)
            {
                this.Entities.Remove(test);
            }
            await _context.SaveChangesAsync();

        }
        public async Task<OperationDetails> Update(Test test, int Id)
        {
            try { 
            var model = this.Entities.Where(s => s.Id == Id).First();
            model.Name = test.Name;
            model.Description = test.Description;
            model.DateOfCreate = test.DateOfCreate;
            model.unitId = test.unitId;
            model.Unit = test.Unit;
            model.Questions = test.Questions;
            model.PassedTestUsers = test.PassedTestUsers;

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
