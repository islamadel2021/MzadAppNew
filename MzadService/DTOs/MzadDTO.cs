namespace MzadService.DTOs;

public class MzadDTO
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; }
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int SoldAmount { get; set; }
    public int CurrentHighTender { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime MzadEnd { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public string Father { get; set; }
    public string Mother { get; set; }
    public string Breed { get; set; }
    public int YearOfBirth { get; set; }
    public string Color { get; set; }
    public string ImageUrl { get; set; }
}
