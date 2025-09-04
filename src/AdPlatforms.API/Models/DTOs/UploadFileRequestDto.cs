using System.ComponentModel.DataAnnotations;

namespace AdPlatforms.API.Models.DTOs;

public class UploadFileRequestDto
{
    [Required]
    public IFormFile File { get; set; }
}
