using System.Net;
using System.Net.Http.Json;
using Contracts;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MzadService.Data;
using MzadService.DTOs;

namespace MzadService.IntegrationTests;
[Collection("Shared collection")]
public class MzadBusTests(CustomWebApplicationFactory factory) : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory = factory;
    private readonly HttpClient _httpClient = factory.CreateClient();
    private readonly ITestHarness _testHarness = factory.Services.GetRequiredService<ITestHarness>();

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MzadDbContext>();
        DbHelper.ReinitDbForTests(db);
        return Task.CompletedTask;
    }

    [Fact]
    public async Task CreateMzad_WithValidObject_ShouldPublishMzadCreatedEvent()
    {
        // arrange
        var mzadForCreateDTO = GetMzadForCreateDTO();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("Muhammad"));

        // act
        var response = await _httpClient.PostAsJsonAsync("api/mzad", mzadForCreateDTO);

        // assert
        response.EnsureSuccessStatusCode();
        Assert.True(await _testHarness.Published.Any<MzadCreated>());
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
