namespace MzadService.DTOs;

public class MzadForUpdateDTO
{
    public string Name { get; set; }
    public string Father { get; set; }
    public string Mother { get; set; }
    public string Breed { get; set; }
    public int? YearOfBirth { get; set; }
    public string Color { get; set; }
    public string ImageUrl { get; set; }
    public int? ReservePrice { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? MzadEnd { get; set; }
}
