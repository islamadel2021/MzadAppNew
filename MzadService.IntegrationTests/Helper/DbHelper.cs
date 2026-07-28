using MzadService.Data;
using MzadService.Entities;

namespace MzadService.IntegrationTests;

public static class DbHelper
{
    public static void InitDbForTests(MzadDbContext db)
    {
        db.Mzadat.AddRange(GetMzadatForTest());
        db.SaveChanges();
    }
    public static void ReinitDbForTests(MzadDbContext db)
    {
        db.Mzadat.RemoveRange(db.Mzadat);
        db.SaveChanges();
        InitDbForTests(db);
    }
    private static List<Mzad> GetMzadatForTest()
    {
        return [
                // 1 Kassam
                new() {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Live,
                ReservePrice = 200000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Kassam",
                    Father = "Ezz",
                    Mother = "Gaza",
            Breed = "Kohylan",
                    YearOfBirth = 2022,
            Color = "White",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/12/03/07/35/horse-2994388_1280.jpg"
                }
            },
            // 2 Anbar
            new() {
                Id = Guid.Parse("f01b70b4-8a1c-4e20-8ba2-9b41c5fe37dc"),
                Status = Status.Live,
                ReservePrice = 100000,
                Seller = "Amir",
                MzadEnd = DateTime.UtcNow.AddDays(90),
                Horse = new Horse
                {
                    Name = "Anbar",
                    Father = "Adl",
                    Mother = "Adlat",
            Breed = "Hadban",
                    YearOfBirth = 2021,
            Color = "White",
                    ImageUrl = "https://cdn.pixabay.com/photo/2018/05/10/09/47/mold-3387158_1280.jpg"
                }
            },
            // 3 Ghanam
            new() {
                Id = Guid.Parse("6d8e2c4d-3b12-47a0-b9bb-4a7d9e3c8f51"),
                Status = Status.Live,
                ReservePrice = 150000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Ghanam",
                    Father = "Gabal",
                    Mother = "Ghalia",
            Breed = "Hamadani",
                    YearOfBirth = 2021,
            Color = "grey",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/10/08/07/15/main-and-state-stud-marbach-2829062_1280.jpg"
                }
            }
        ];
    }

}
