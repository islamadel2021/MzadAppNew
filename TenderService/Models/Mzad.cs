using MongoDB.Entities;

namespace TenderService;

public class Mzad : Entity
{
    public string Seller { get; set; }
    public int ReservePrice { get; set; }
    public bool Finished { get; set; }
    public DateTime MzadEnd { get; set; } = DateTime.UtcNow;
}
