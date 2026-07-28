namespace Contracts;

public class TenderPlaced
{
    public string Id { get; set; }
    public string MzadId { get; set; }
    public string TenderOwner { get; set; }
    public DateTime TenderTime { get; set; }
    public string TenderStatus { get; set; }
    public int Amount { get; set; }
}
