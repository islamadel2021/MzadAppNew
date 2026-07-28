using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MzadService.Data;
using MzadService.DTOs;

namespace MzadService.IntegrationTests;
[Collection("Shared collection")]
public class MzadControllerTests(CustomWebApplicationFactory factory) : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory = factory;
    private readonly HttpClient _httpClient = factory.CreateClient();
    private const string _kassam_ID = "afbee524-5972-4075-8800-7d1f9d7b0a0c";

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MzadDbContext>();
        DbHelper.ReinitDbForTests(db);
        return Task.CompletedTask;
    }

    [Fact]
    public async Task GetMzadat_ShouldReturn3Mzadat()
    {
        // arrange?

        // act
        var response = await _httpClient.GetFromJsonAsync<List<MzadDTO>>("api/mzad");

        // assert
        Assert.Equal(3, response.Count);
    }

    [Fact]
    public async Task GetMzadById_WithValidId_ShouldReturnMzad()
    {
        // arrange? 

        // act
        var response = await _httpClient.GetFromJsonAsync<MzadDTO>($"api/mzad/{_kassam_ID}");

        // assert
        Assert.Equal("Kassam", response.Name);
    }

    [Fact]
    public async Task GetMzadById_WithInvalidId_ShouldReturn404()
    {
        // arrange? 

        // act
        var response = await _httpClient.GetAsync($"api/mzad/{Guid.NewGuid()}");

        // assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMzadById_WithInvalidGuid_ShouldReturn400()
    {
        // arrange? 

        // act
        var response = await _httpClient.GetAsync($"api/mzad/notaguid");

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateMzad_WithNoAuth_ShouldReturn401()
    {
        // arrange 
        var mzadForCreateDTO = new MzadForCreateDTO { Name = "Test" };

        // act
        var response = await _httpClient.PostAsJsonAsync($"api/mzad", mzadForCreateDTO);

        // assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateMzad_WithAuth_ShouldReturn201()
    {
        // arrange 
        var mzadForCreateDTO = GetMzadForCreateDTO();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("Tester"));

        // act
        var response = await _httpClient.PostAsJsonAsync($"api/mzad", mzadForCreateDTO);

        // assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdMzad = await response.Content.ReadFromJsonAsync<MzadDTO>();
        Assert.Equal("Tester", createdMzad.Seller);
    }

    [Fact]
    public async Task CreateMzad_WithInvalidMzadForCreateDTO_ShouldReturn400()
    {
        // arrange? 
        var mzadForCreateDTO = GetMzadForCreateDTO();
        mzadForCreateDTO.Name = null;
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("Muhammad"));

        // act
        var response = await _httpClient.PostAsJsonAsync($"api/mzad", mzadForCreateDTO);

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateMzad_WithValidMzadForUpdateDTOAndUser_ShouldReturn200()
    {
        // arrange? 
        var mzadForUpdateDTO = new MzadForUpdateDTO { Name = "Updated" };
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("Muhammad"));

        // act
        var response = await _httpClient.PutAsJsonAsync($"api/mzad/{_kassam_ID}", mzadForUpdateDTO);

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateMzad_WithValidmzadForUpdateDTOAndInvalidUser_ShouldReturn403()
    {
        // arrange? 
        var mzadForUpdateDTO = new MzadForUpdateDTO { Name = "Updated" };
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("NotMuhammad"));

        // act
        var response = await _httpClient.PutAsJsonAsync($"api/mzad/{_kassam_ID}", mzadForUpdateDTO);

        // assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }


    private static MzadForCreateDTO GetMzadForCreateDTO()
    {
        return new MzadForCreateDTO
        {
            Name = "Test",
            Father = "Test",
            Mother = "Test",
            Breed = "Test",
            YearOfBirth = 2000,
            Color = "Test",
            ImageUrl = "Test",
            ReservePrice = 1000,
            MzadEnd = DateTime.UtcNow
        };
    }

}

