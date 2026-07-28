using FilterService.Entities;
using MongoDB.Entities;

namespace FilterService.Services;

public class MzadSvcHttpClient(HttpClient httpClient, IConfiguration configuration)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<Mzad>> GetMzadatForFilterDb()
    {
        var lastUpdateDate = await DB.Find<Mzad, string>()
        .Sort(s => s.Descending(m => m.UpdatedAt))
        .Project(m => m.UpdatedAt.ToString())
        .ExecuteFirstAsync();

        var mzadat = await _httpClient.GetFromJsonAsync<List<Mzad>>(_configuration["MzadServiceUrl"] + "/api/mzad?lastUpdateDate=" + lastUpdateDate);
        return mzadat;
    }
}
