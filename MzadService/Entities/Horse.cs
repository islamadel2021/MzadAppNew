using System.ComponentModel.DataAnnotations.Schema;

namespace MzadService.Entities;

[Table("Horses")]
public class Horse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Father { get; set; }
    public string Mother { get; set; }
    public string Breed { get; set; }
    public int YearOfBirth { get; set; }
    public string Color { get; set; }
    public string ImageUrl { get; set; }

    // navigation properties
    public Mzad Mzad { get; set; }
    public Guid MzadId { get; set; }
}
