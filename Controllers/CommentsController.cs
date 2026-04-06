using System.Security.Claims;
using emlak_son.DTOs.Comment;
using emlak_son.Models;
using emlak_son.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace emlak_son.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IGenericRepository<Comment> _commentRepository;
    private readonly IGenericRepository<Property> _propertyRepository;

    public CommentsController(IGenericRepository<Comment> commentRepository, IGenericRepository<Property> propertyRepository)
    {
        _commentRepository = commentRepository;
        _propertyRepository = propertyRepository;
    }

    [HttpGet("property/{propertyId:int}")]
    public async Task<ActionResult<IEnumerable<ResultCommentDto>>> GetByProperty(int propertyId)
    {
        var comments = await _commentRepository.FindAsync(c => c.PropertyId == propertyId && c.IsApproved);
        return Ok(comments.Select(c => new ResultCommentDto
        {
            Id = c.Id,
            Content = c.Content,
            IsApproved = c.IsApproved,
            CreatedDate = c.CreatedDate,
            AppUserId = c.AppUserId,
            PropertyId = c.PropertyId
        }));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentDto dto)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId)) return Unauthorized();

        var property = await _propertyRepository.GetByIdAsync(dto.PropertyId);
        if (property == null) return NotFound("Property not found");

        var comment = new Comment
        {
            PropertyId = dto.PropertyId,
            AppUserId = userId,
            Content = dto.Content,
            IsApproved = false // Requires admin approval by default
        };

        await _commentRepository.InsertAsync(comment);
        await _commentRepository.SaveChangesAsync();

        return Ok(new ResultCommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            IsApproved = comment.IsApproved,
            CreatedDate = comment.CreatedDate,
            AppUserId = comment.AppUserId,
            PropertyId = comment.PropertyId
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<ResultCommentDto>>> GetPending()
    {
        var comments = await _commentRepository.FindAsync(c => !c.IsApproved);
        return Ok(comments.Select(c => new ResultCommentDto
        {
            Id = c.Id,
            Content = c.Content,
            IsApproved = c.IsApproved,
            CreatedDate = c.CreatedDate,
            AppUserId = c.AppUserId,
            PropertyId = c.PropertyId
        }));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/approve")]
    public async Task<IActionResult> Approve(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();

        comment.IsApproved = true;
        _commentRepository.Update(comment);
        await _commentRepository.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();

        _commentRepository.Delete(comment);
        await _commentRepository.SaveChangesAsync();

        return NoContent();
    }
}
