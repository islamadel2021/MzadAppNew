using MongoDB.Entities;

namespace TenderService;

public class Tender : Entity
{
    public string MzadId { get; set; }
    public string TenderOwner { get; set; }
    public int Amount { get; set; }
    public DateTime TenderTime { get; set; } = DateTime.UtcNow;
    public TenderStatus TenderStatus { get; set; }
}
