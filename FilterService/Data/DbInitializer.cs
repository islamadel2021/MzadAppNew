using FilterService.Entities;
using FilterService.Services;
using MongoDB.Driver;
using MongoDB.Entities;

namespace FilterService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("FilterDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDb")));

        await DB.Index<Mzad>()
        .Key(m => m.Seller, KeyType.Text)
        .Key(m => m.Breed, KeyType.Text)
        .Key(m => m.Color, KeyType.Text)
        .CreateAsync();

        using var scope = app.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetService<MzadSvcHttpClient>();
        var mzadat = await httpClient.GetMzadatForFilterDb();
        Console.WriteLine($"{mzadat.Count} Mzad Retrieved from Mzad Service");
        await DB.SaveAsync(mzadat);
    }
}
