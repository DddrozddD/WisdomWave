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
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)

    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAsyncs();
        return new JsonResult(categories);
    }
    [HttpGet("without-parents")]
    public async Task<IActionResult> GetCategoriesWithoutParents()
    {
        var categories = await _categoryService.GetCategoriesWithoutParentAsync();
        return new JsonResult(categories);
    }

    [HttpGet("without-children")]
    public async Task<IActionResult> GetCategoriesWithoutChildren()
    {
        var categories = await _categoryService.GetCategoriesWithoutChildAsync();
        return new JsonResult(categories);
    }

    [HttpGet("with-children&parents")]
    public async Task<IActionResult> GetCategoriesWithPerentsChild()
    {
        var categories = await _categoryService.GetCategoriesWithParentChild();
        return new JsonResult(categories);
    }

    [HttpGet("child-categories/{categoryName}")]
    public async Task<IActionResult> GetChildCategories(string categoryName)
    {
        var categories = await _categoryService.GetCategoriesByParentNameAsync(categoryName);
        return new JsonResult(categories);
    }

   [HttpGet("parent-categories/{categoryName}")]
    public async Task<IActionResult> GetParentCategories(string categoryName)
    {
        var categories = await _categoryService.GetCategoriesByChildNameAsync(categoryName);
        return new JsonResult(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _categoryService.FindByConditionItemAsync(c => c.Id == id);


        if (category == null)
        {
            return NotFound();
        }
        return new JsonResult(category);
    }

    [HttpPost("{courseId}")]
    public async Task<IActionResult> CreateCategory([FromBody] Category category, int courseId)
    {

        var result = await _categoryService.CreateAsync(category, courseId);

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

        var result = await _categoryService.EditAsync(id, category);

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

}
