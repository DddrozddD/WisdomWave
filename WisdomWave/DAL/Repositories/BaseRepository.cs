using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _enteiies;
        protected WwContext _context;
        protected DbSet<TEntity> Entities => this._enteiies ??= _context.Set<TEntity>();
        protected BaseRepository(WwContext context) => _context = context;
        public async Task<OperationDetails> CreateAsync(TEntity entity)
        {
            try
            {
                await this.Entities.AddAsync(entity).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return new OperationDetails { Message = "Created" };
            }
            catch (Exception ex)
            {
                return new OperationDetails { Message = "Create Fatal Error", exception=ex, IsError=true };
            }
        }
       
        public  async Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicat) => 
            await this.Entities.Where(predicat).ToListAsync().ConfigureAwait(false);
        
        public  async Task<TEntity> FindByConditionItemAsync(Expression<Func<TEntity, bool>> predicat) =>
           await this.Entities.FirstOrDefaultAsync(predicat);
        public  async Task<IReadOnlyCollection<TEntity>> GetAllAsync()=> await this.Entities.ToListAsync().ConfigureAwait(false);
        
    }
}
