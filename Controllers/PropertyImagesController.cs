using emlak_son.DTOs.PropertyImage;
using emlak_son.Models;
using emlak_son.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace emlak_son.Controllers;

[ApiController]
[Route("api/properties/{propertyId:int}/images")]
public class PropertyImagesController : ControllerBase
{
    private readonly IGenericRepository<PropertyImage> _imageRepository;
    private readonly IGenericRepository<Property> _propertyRepository;
    private readonly IWebHostEnvironment _env;

    public PropertyImagesController(IGenericRepository<PropertyImage> imageRepository, IGenericRepository<Property> propertyRepository, IWebHostEnvironment env)
    {
        _imageRepository = imageRepository;
        _propertyRepository = propertyRepository;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultPropertyImageDto>>> GetByProperty(int propertyId)
    {
        var images = await _imageRepository.FindAsync(i => i.PropertyId == propertyId);
        return Ok(images.Select(Map));
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(int propertyId, IFormFile file, [FromForm] bool isMain = false)
    {
        if (file is null || file.Length == 0) return BadRequest("No file provided");

        var property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property is null) return NotFound("Property not found.");

        var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "properties");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        if (isMain)
        {
            var existingImages = await _imageRepository.FindAsync(i => i.PropertyId == propertyId && i.IsMain);
            foreach (var img in existingImages)
            {
                img.IsMain = false;
                _imageRepository.Update(img);
            }
        }

        var newImage = new PropertyImage
        {
            PropertyId = propertyId,
            ImageUrl = $"/images/properties/{uniqueFileName}",
            IsMain = isMain
        };

        await _imageRepository.InsertAsync(newImage);
        await _imageRepository.SaveChangesAsync();

        return Ok(Map(newImage));
    }

    [Authorize(Roles = "User,Admin")]
    [HttpDelete("{imageId:int}")]
    public async Task<IActionResult> Delete(int propertyId, int imageId)
    {
        var image = await _imageRepository.GetByIdAsync(imageId);
        if (image is null || image.PropertyId != propertyId) return NotFound();

        _imageRepository.Delete(image);
        await _imageRepository.SaveChangesAsync();
        return NoContent();
    }

    private static ResultPropertyImageDto Map(PropertyImage image) => new()
    {
        Id = image.Id,
        ImageUrl = image.ImageUrl,
        IsMain = image.IsMain,
        PropertyId = image.PropertyId
    };
}
