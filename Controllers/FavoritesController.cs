using System.Security.Claims;
using emlak_son.DTOs.Favorite;
using emlak_son.Models;
using emlak_son.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace emlak_son.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IGenericRepository<Favorite> _favoriteRepository;
    private readonly IGenericRepository<Property> _propertyRepository;

    public FavoritesController(IGenericRepository<Favorite> favoriteRepository, IGenericRepository<Property> propertyRepository)
    {
        _favoriteRepository = favoriteRepository;
        _propertyRepository = propertyRepository;
    }

    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<ResultFavoriteDto>>> GetMine()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId)) return Unauthorized();

        var favorites = await _favoriteRepository.FindAsync(f => f.AppUserId == userId);
        return Ok(favorites.Select(f => new ResultFavoriteDto
        {
            Id = f.Id,
            AppUserId = f.AppUserId,
            PropertyId = f.PropertyId,
            CreatedDate = f.CreatedDate
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateFavoriteDto dto)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId)) return Unauthorized();

        var property = await _propertyRepository.GetByIdAsync(dto.PropertyId);
        if (property == null) return NotFound("Property not found");

        var exists = await _favoriteRepository.FindAsync(f => f.AppUserId == userId && f.PropertyId == dto.PropertyId);
        if (exists.Any()) return BadRequest("Already in favorites");

        var fav = new Favorite { AppUserId = userId, PropertyId = dto.PropertyId };
        await _favoriteRepository.InsertAsync(fav);
        await _favoriteRepository.SaveChangesAsync();

        return Ok(new ResultFavoriteDto { Id = fav.Id, AppUserId = fav.AppUserId, PropertyId = fav.PropertyId, CreatedDate = fav.CreatedDate });
    }

    [HttpDelete("property/{propertyId:int}")]
    public async Task<IActionResult> Remove(int propertyId)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId)) return Unauthorized();

        var favs = await _favoriteRepository.FindAsync(f => f.AppUserId == userId && f.PropertyId == propertyId);
        var fav = favs.FirstOrDefault();
        if (fav == null) return NotFound();

        _favoriteRepository.Delete(fav);
        await _favoriteRepository.SaveChangesAsync();

        return NoContent();
    }
}
