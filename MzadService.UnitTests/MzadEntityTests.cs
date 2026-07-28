using MzadService.Entities;

namespace MzadService.UnitTests;

public class MzadEntityTests
{
    [Fact]
    public void HasReservePrice_ReservePriceGtZero_True()
    {
        // Arrange
        var mzad = new Mzad { Id = Guid.NewGuid(), ReservePrice = 100 };

        // Act
        var result = mzad.HasReservePrice();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasReservePrice_ReservePriceIsZero_False()
    {
        // Arrange
        var mzad = new Mzad { Id = Guid.NewGuid(), ReservePrice = 0 };

        // Act
        var result = mzad.HasReservePrice();

        // Assert
        Assert.False(result);
    }
}
