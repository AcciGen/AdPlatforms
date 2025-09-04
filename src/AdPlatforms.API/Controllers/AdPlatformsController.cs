using AdPlatforms.API.Models.DTOs;
using AdPlatforms.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatforms.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdPlatformsController : ControllerBase
{
    private readonly IAdPlatformService _service;

    public AdPlatformsController(IAdPlatformService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAsync([FromForm] UploadFileRequestDto request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("Файл не загружен либо пустой");

        string fileContent;
        using (var reader = new StreamReader(request.File.OpenReadStream()))
        {
            fileContent = await reader.ReadToEndAsync();
        }
        _service.LoadFromFile(fileContent);

        return Ok("Файл успешно загружен и обработан");
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string location)
    {
        if (string.IsNullOrWhiteSpace(location)) return BadRequest("Локация не указана");

        var platforms = _service.FindByLocation(location);

        if (platforms.Count() == 0)
            return NotFound($"Нет площадок для локации: {location}");

        return Ok(platforms);
    }
}
