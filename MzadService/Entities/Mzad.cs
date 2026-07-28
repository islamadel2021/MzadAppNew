namespace MzadService.Entities;

public class Mzad
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighTender { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime MzadEnd { get; set; }
    public Status Status { get; set; }
    public Horse Horse { get; set; }
    public bool HasReservePrice() => ReservePrice > 0;
}
