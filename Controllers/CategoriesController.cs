using emlak_son.DTOs.Category;
using emlak_son.Entities;
using emlak_son.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace emlak_son.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoriesController(IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultCategoryDto>>> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var result = categories.Select(c => new ResultCategoryDto
        {
            Id = c.Id,
            CategoryName = c.CategoryName,
            Status = c.Status
        });

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResultCategoryDto>> GetById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        return Ok(new ResultCategoryDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Status = category.Status
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var category = new Category
        {
            CategoryName = dto.CategoryName,
            Status = dto.Status
        };

        await _categoryRepository.InsertAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, new ResultCategoryDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Status = category.Status
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        category.CategoryName = dto.CategoryName;
        category.Status = dto.Status;

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        _categoryRepository.Delete(category);
        await _categoryRepository.SaveChangesAsync();

        return NoContent();
    }
}
