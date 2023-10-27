using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using Domain.Models;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAsyncs();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        await _categoryService.CreateAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {
        await _categoryService.UpdateAsync(id, category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
    // Отображение категорий без родительских категорий
    [HttpGet("WithoutParent")]
    public async Task<IActionResult> GetCategoriesWithoutParent()
    {
        var categories = await _categoryService.GetCategoriesWithoutParentAsync();
        return Ok(categories);
    }

    // Отображение категорий, которые имеют переданный ID родительской категории
    [HttpGet("ByParentId/{parentId}")]
    public async Task<IActionResult> GetCategoriesByParentId(int parentId)
    {
        var categories = await _categoryService.GetCategoriesByParentIdAsync(parentId);
        return Ok(categories);
    }

    // Отображение всех родительских категорий для переданной категории по ID
    [HttpGet("ParentCategories/{categoryId}")]
    public async Task<IActionResult> GetParentCategoriesById(int categoryId)
    {
        var parentCategories = await _categoryService.GetParentCategoriesByIdAsync(categoryId);
        return Ok(parentCategories);
    }

}
