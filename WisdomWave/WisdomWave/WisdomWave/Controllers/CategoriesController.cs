using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Domain.Models;
using BLL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
/*    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)

    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAsyncs();
    [HttpGet("without-parents")]
    public async Task<IActionResult> GetCategoriesWithoutParents()
    {
        var categories = await _categoryService.GetCategoriesWithoutParentsAsync();
        return Ok(categories);
    }

    [HttpGet("child-categories/{parentId}")]
    public async Task<IActionResult> GetChildCategories(int parentId)
    {
        var categories = await _categoryService.GetChildCategoriesAsync(parentId);
        return Ok(categories);
    }

    [HttpGet("parent-categories/{categoryId}")]
    public async Task<IActionResult> GetParentCategories(int categoryId)
    {
        var categories = await _categoryService.GetParentCategoriesAsync(categoryId);
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

        var result = await _categoryService.CreateCategoryAsync(category);

        if (result.IsError == false)
        {
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        return BadRequest(result.Message);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {


        if (id != category.Id)
        {
            return BadRequest();
        }

        var result = await _categoryService.UpdateCategoryAsync(category);

        if (result.IsError == false)
        {
            return NoContent();
        }

        return BadRequest(result.Message);

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

    [HttpGet("FindByIdInList/{categoryId}")]
    public async Task<IActionResult> FindCategoryByIdInList(int categoryId, [FromQuery] List<Category> categoryList)
    {
        var category = await _categoryService.FindCategoryByIdAsync(categoryId, categoryList);

        if (category != null)
        {
            return Ok(category);
        }

        return NotFound();
    }

        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }*/

}
