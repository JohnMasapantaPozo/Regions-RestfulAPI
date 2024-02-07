using System.ComponentModel.DataAnnotations;

namespace RestfulDEMO.API.Models.Dtos
{
    public class UpdateRegionRequestDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string? RegionImageUrl { get; set; }
    }
}
