using Microsoft.EntityFrameworkCore;
using MzadService.Entities;

namespace MzadService.Data;

public class DbInitializer
{

    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<MzadDbContext>());
    }

    private static void SeedData(MzadDbContext context)
    {
        context.Database.Migrate();

        if (context.Mzadat.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var Mzadat = new List<Mzad>()
        {
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
            },
            // 4 Rawy
            new() {
                Id = Guid.Parse("a5f7e9d2-91c3-4c7e-83f8-2e0a0b6e227f"),
                Status = Status.Live,
                ReservePrice = 120000,
                Seller = "Amany",
                MzadEnd = DateTime.UtcNow.AddDays(80),
                Horse = new Horse
                {
                    Name = "Rawy",
                    Father = "Rashdan",
                    Mother = "Rawia",
            Breed = "Saklawy",
                    YearOfBirth = 2021,
            Color = "brown",
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/05/02/12/32/arabs-1367173_1280.jpg"
                }
            },
            // 5 Shokr
            new() {
                Id = Guid.Parse("9f3a8f6b-6c59-4c78-89a2-2d3f37d5f3e2"),
                Status = Status.Live,
                ReservePrice = 100000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Shokr",
                    Father = "Shaker",
                    Mother = "Shokran",
            Breed = "Hadaban",
                    YearOfBirth = 2022,
            Color = "White",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/09/20/19/50/horse-948710_1280.jpg"
                }
            },
            // 6 Fady
            new() {
                Id = Guid.Parse("c1d49db7-3aa7-49a9-8a75-aae43204a7c9"),
                Status = Status.Live,
                ReservePrice = 110000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Fady",
                    Father = "Foaad",
                    Mother = "Fadya",
            Breed = "Hadaban",
                    YearOfBirth = 2021,
            Color = "brown",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/06/12/11/37/arabian-2395267_1280.jpg"
                }
            },
            // 7 Raad
            new() {
                Id = Guid.Parse("bae0f83d-2b6c-4e2a-9df7-1c3f4b7bd59d"),
                Status = Status.Live,
                ReservePrice = 112000,
                Seller = "Amir",
                MzadEnd = DateTime.UtcNow.AddDays(90),
                Horse = new Horse
                {
                    Name = "Raad",
                    Father = "Raadan",
                    Mother = "Raada",
            Breed = "Kohylan",
                    YearOfBirth = 2022,
            Color = "brown",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/03/18/04/13/horse-678734_1280.jpg"
                }
            },
            // 8 Defaf
            new() {
                Id = Guid.Parse("8f6e3a1d-c5f8-48e4-ae0b-72c9c164e566"),
                Status = Status.Live,
                ReservePrice = 200000,
                Seller = "Al-Ahmad Station",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Defaf",
                    Father = "Deef",
                    Mother = "Wadfa",
            Breed = "Saklawy",
                    YearOfBirth = 2020,
            Color = "brown",
                    ImageUrl = "https://cdn.pixabay.com/photo/2019/10/15/19/06/arabian-4552669_1280.jpg"
                }
            },
            // 9 Helm
            new() {
                Id = Guid.Parse("d4d6f845-732c-41a7-b39a-9e4e8a31d569"),
                Status = Status.Live,
                ReservePrice = 130000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Helm",
                    Father = "Halim",
                    Mother = "Halima",
            Breed = "Kohylan",
                    YearOfBirth = 2021,
            Color = "grey",
                    ImageUrl = "https://cdn.pixabay.com/photo/2018/05/27/16/16/arabian-horse-3433815_1280.jpg"
                }
            },
            // 10 Karem
            new() {
                Id = Guid.Parse("7b0f9a8c-d205-4a84-8398-cf3bf2378bfe"),
                Status = Status.Live,
                ReservePrice = 200000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(90),
                Horse = new Horse
                {
                    Name = "Karem",
                    Father = "Karam",
                    Mother = "Karima",
            Breed = "Hamadani",
                    YearOfBirth = 2021,
            Color = "grey",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/05/27/15/47/horse-2348766_1280.jpg"
                }
            },
	    // 11 Bassam
            new() {
                Id = Guid.Parse("a2fc45e1-7a09-45fd-8e3f-daae2f2d6e7e"),
                Status = Status.Live,
                ReservePrice = 200000,
                Seller = "Al-Ahmad Station",
                MzadEnd = DateTime.UtcNow.AddDays(100),
                Horse = new Horse
                {
                    Name = "Bassam",
                    Father = "Basem",
                    Mother = "Basma",
            Breed = "Hadaban",
                    YearOfBirth = 2020,
            Color = "brown",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/11/05/19/59/horse-1024716_1280.jpg"
                }
            },
            // 12 Antar
            new() {
                Id = Guid.Parse("4e6a18b1-fa62-47e7-9ef7-268a1d2ea48f"),
                Status = Status.Live,
                ReservePrice = 300000,
                Seller = "Muhammad",
                MzadEnd = DateTime.UtcNow.AddDays(110),
                Horse = new Horse
                {
                    Name = "Antar",
                    Father = "Fares",
                    Mother = "Farasa",
            Breed = "Kohylan",
                    YearOfBirth = 2020,
            Color = "black",
                    ImageUrl = "https://cdn.pixabay.com/photo/2020/07/03/19/58/rap-5367452_1280.jpg"
                }
            }
        };
        context.Mzadat.AddRange(Mzadat);
        context.SaveChanges();
    }
}
