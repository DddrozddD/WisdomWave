using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using DAL.Repositories;
using DAL.Models;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyCollection<Category>> GetCategoriesWithoutParentsAsync()
    {
        return (await _categoryRepository.GetAllAsync()).Where(c => c.ParentCategoryId == null).ToList();
    }

    public async Task<IReadOnlyCollection<Category>> GetChildCategoriesAsync(int parentId)
    {
        return (await _categoryRepository.GetAllAsync()).Where(c => c.ParentCategoryId == parentId).ToList();
    }

    public async Task<IReadOnlyCollection<Category>> GetParentCategoriesAsync(int categoryId)
    {
        var parentCategories = new List<Category>();
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        while (category != null)
        {
            parentCategories.Add(category);
            category = await _categoryRepository.GetByIdAsync((int)category.ParentCategoryId);
        }

        parentCategories.Reverse();
        return parentCategories;
    }

    public async Task<Category> GetCategoryAsync(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<OperationDetails> CreateCategoryAsync(Category category)
    {
        await _categoryRepository.CreateAsync(category);
        return new OperationDetails { Message = "Category created successfully" };
    }

    public async Task<OperationDetails> UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);

        if (existingCategory == null)
        {
            return new OperationDetails { Message = "Create Fatal Error", IsError = true };
        }

        existingCategory.CategoryName = category.CategoryName;
        existingCategory.ParentCategoryId = category.ParentCategoryId;

        await _categoryRepository.UpdateAsync(existingCategory);
        return new  OperationDetails{ Message = "Category updated successfully" };
    }

    public async Task<OperationDetails> DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return new OperationDetails{ Message = "Category not found", IsError = true };
        }

        await _categoryRepository.DeleteAsync(id);
        return new OperationDetails{ Message = "Category deleted successfully" };
    }
}
