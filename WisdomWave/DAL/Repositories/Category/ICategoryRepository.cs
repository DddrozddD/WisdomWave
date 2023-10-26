using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> FindByConditionAsync(Expression<Func<Category, bool>> predicate);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
