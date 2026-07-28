using System.ComponentModel.DataAnnotations;

namespace MzadService.DTOs;

public class MzadForCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Father { get; set; }
    [Required]
    public string Mother { get; set; }
    [Required]
    public string Breed { get; set; }
    [Required]
    public int YearOfBirth { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]
    public int ReservePrice { get; set; }
    [Required]
    public DateTime MzadEnd { get; set; }

}
