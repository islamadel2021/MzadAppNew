using AutoFixture;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MzadService.Controllers;
using MzadService.DTOs;
using MzadService.Entities;
using MzadService.RequestHelpers;

namespace MzadService.UnitTests;

public class MzadControllerTests
{
    private readonly Mock<IMzadRepository> _mzadRepo;
    private readonly Mock<IPublishEndpoint> _publishEndpoint;
    private readonly Fixture _fixture;
    private readonly MzadController _mzadController;
    private readonly IMapper _mapper;
    public MzadControllerTests()
    {
        _fixture = new Fixture();
        _mzadRepo = new Mock<IMzadRepository>();
        _publishEndpoint = new Mock<IPublishEndpoint>();
        var mockMapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly)).CreateMapper().ConfigurationProvider;
        _mapper = new Mapper(mockMapper);
        _mzadController = new MzadController(_mzadRepo.Object, _mapper, _publishEndpoint.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = Helper.GetClaimsPrincipal()
                }
            }
        };
    }

    [Fact]
    public async Task GetMzadat_WithNoParams_Returns10Mzadat()
    {
        // Arrange
        var mzadat = _fixture.CreateMany<MzadDTO>(10).ToList();
        _mzadRepo.Setup(repo => repo.GetMzadatAsync(null)).ReturnsAsync(mzadat);

        // Act
        var result = await _mzadController.GetMzadat(null);

        // Assert
        Assert.Equal(10, result.Count());
        Assert.IsType<List<MzadDTO>>(result);
    }

    [Fact]
    public async Task GetMzadById_WithValidId_ReturnsMzad()
    {
        // Arrange
        var mzad = _fixture.Create<MzadDTO>();
        _mzadRepo.Setup(repo => repo.GetMzadByIdAsync(mzad.Id)).ReturnsAsync(mzad);

        // Act
        var result = await _mzadController.GetMzadById(mzad.Id);

        // Assert
        Assert.IsType<ActionResult<MzadDTO>>(result);
        Assert.Equal(mzad.Name, result.Value.Name);
    }

    [Fact]
    public async Task GetMzadById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange

        _mzadRepo.Setup(repo => repo.GetMzadByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

        // Act
        var result = await _mzadController.GetMzadById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateMzad_WithValidMzadForCreateDTO_ReturnsCreatedAtAction()
    {
        // arrange
        var mzadForCreateDTO = _fixture.Create<MzadForCreateDTO>();
        _mzadRepo.Setup(repo => repo.AddMzad(It.IsAny<Mzad>()));
        _mzadRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        // act
        var result = await _mzadController.CreateMzad(mzadForCreateDTO);
        var createdResult = result.Result as CreatedAtActionResult;

        // assert
        Assert.NotNull(createdResult);
        Assert.Equal("GetMzadById", createdResult.ActionName);
        Assert.IsType<MzadDTO>(createdResult.Value);
    }

    [Fact]
    public async Task CreateMzad_FailedSave_Returns400BadRequest()
    {
        // arrange
        var mzadForCreateDTO = _fixture.Create<MzadForCreateDTO>();
        _mzadRepo.Setup(repo => repo.AddMzad(It.IsAny<Mzad>()));
        _mzadRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(false);

        // act
        var result = await _mzadController.CreateMzad(mzadForCreateDTO);

        // assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateMzad_WithUpdateMzadDto_ReturnsOkResponse()
    {
        // arrange
        var mzad = _fixture.Build<Mzad>().Without(x => x.Horse).Create();
        mzad.Horse = _fixture.Build<Horse>().Without(x => x.Mzad).Create();
        mzad.Seller = "test";
        var mzadForUpdateDTO = _fixture.Create<MzadForUpdateDTO>();
        _mzadRepo.Setup(repo => repo.GetMzadEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(mzad);
        _mzadRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        // act
        var result = await _mzadController.UpdateMzad(mzad.Id, mzadForUpdateDTO);

        // assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateMzad_WithInvalidUser_Returns403Forbid()
    {
        // arrange
        var mzad = _fixture.Build<Mzad>().Without(x => x.Horse).Create();
        mzad.Seller = "not-test";
        var mzadForUpdateDTO = _fixture.Create<MzadForUpdateDTO>();
        _mzadRepo.Setup(repo => repo.GetMzadEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(mzad);

        // act
        var result = await _mzadController.UpdateMzad(mzad.Id, mzadForUpdateDTO);

        // assert
        Assert.IsType<ForbidResult>(result.Result);
    }

    [Fact]
    public async Task UpdateMzad_WithInvalidGuid_ReturnsNotFound()
    {
        // arrange
        var mzad = _fixture.Build<Mzad>().Without(x => x.Horse).Create();
        var mzadForUpdateDTO = _fixture.Create<MzadForUpdateDTO>();
        _mzadRepo.Setup(repo => repo.GetMzadEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // act
        var result = await _mzadController.UpdateMzad(mzad.Id, mzadForUpdateDTO);

        // assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteMzad_WithValidUser_ReturnsOkResponse()
    {
        // arrange
        var mzad = _fixture.Build<Mzad>().Without(x => x.Horse).Create();
        mzad.Seller = "test";

        _mzadRepo.Setup(repo => repo.GetMzadEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(mzad);
        _mzadRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

        // act
        var result = await _mzadController.DeleteMzad(mzad.Id);

        // assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMzad_WithInvalidGuid_Returns404Response()
    {
        // arrange
        var mzad = _fixture.Build<Mzad>().Without(x => x.Horse).Create();
        _mzadRepo.Setup(repo => repo.GetMzadEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // act
        var result = await _mzadController.DeleteMzad(mzad.Id);

        // assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMzad_WithInvalidUser_Returns403Response()
    {
        // arrange
        var mzad = _fixture.Build<Mzad>().Without(x => x.Horse).Create();
        mzad.Seller = "not-test";
        _mzadRepo.Setup(repo => repo.GetMzadEntityById(It.IsAny<Guid>()))
            .ReturnsAsync(mzad);

        // act
        var result = await _mzadController.DeleteMzad(mzad.Id);

        // assert
        Assert.IsType<ForbidResult>(result);
    }
}
