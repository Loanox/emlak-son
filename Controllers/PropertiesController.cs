using System.Security.Claims;
using emlak_son.DTOs.Property;
using emlak_son.Models;
using emlak_son.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace emlak_son.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IGenericRepository<Property> _propertyRepository;

    public PropertiesController(IGenericRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultPropertyDto>>> GetAll()
    {
        var properties = await _propertyRepository.GetAllAsync();
        return Ok(properties.Select(MapToResultDto));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResultPropertyDto>> GetById(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property is null)
        {
            return NotFound();
        }

        return Ok(MapToResultDto(property));
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<ResultPropertyDto>>> GetByCategory(int categoryId)
    {
        var properties = await _propertyRepository.FindAsync(p => p.CategoryId == categoryId);
        return Ok(properties.Select(MapToResultDto));
    }

    [Authorize(Roles = "User")]
    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<ResultPropertyDto>>> GetMine()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var properties = await _propertyRepository.FindAsync(p => p.AppUserId == userId);
        return Ok(properties.Select(MapToResultDto));
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePropertyDto dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var property = new Property
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            SquareMeters = dto.SquareMeters,
            RoomCount = dto.RoomCount,
            City = dto.City,
            District = dto.District,
            Address = dto.Address,
            CategoryId = dto.CategoryId,
            AppUserId = userId,
            CreatedDate = DateTime.UtcNow
        };

        await _propertyRepository.InsertAsync(property);
        await _propertyRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = property.Id }, MapToResultDto(property));
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePropertyDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property is null)
        {
            return NotFound();
        }

        if (!CanAccessProperty(property))
        {
            return Forbid();
        }

        property.Title = dto.Title;
        property.Description = dto.Description;
        property.Price = dto.Price;
        property.SquareMeters = dto.SquareMeters;
        property.RoomCount = dto.RoomCount;
        property.City = dto.City;
        property.District = dto.District;
        property.Address = dto.Address;
        property.CategoryId = dto.CategoryId;

        _propertyRepository.Update(property);
        await _propertyRepository.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property is null)
        {
            return NotFound();
        }

        if (!CanAccessProperty(property))
        {
            return Forbid();
        }

        _propertyRepository.Delete(property);
        await _propertyRepository.SaveChangesAsync();

        return NoContent();
    }

    private bool CanAccessProperty(Property property)
    {
        if (User.IsInRole("Admin"))
        {
            return true;
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(userIdClaim, out var userId) && property.AppUserId == userId;
    }

    private static ResultPropertyDto MapToResultDto(Property property)
    {
        return new ResultPropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            SquareMeters = property.SquareMeters,
            RoomCount = property.RoomCount,
            City = property.City,
            District = property.District,
            Address = property.Address,
            CreatedDate = property.CreatedDate,
            CategoryId = property.CategoryId,
            AppUserId = property.AppUserId
        };
    }
}
