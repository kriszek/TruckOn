namespace TruckOn.Trucks.Application.Tests;

public class TrucksServiceTests
{
    [Theory, AutoDomainData]
    public async void CreateTruck_ReturnsTrue_WhenTruckDoesNotExist_AndSaveSuccessful(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, Truck truck)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync((Truck?)null);
        truckReposotory.Setup(r => r.Create(truck)).ReturnsAsync(true);

        /// Act
        var result = await sut.CreateTruck(truck);

        /// Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeTrue();
    }

    [Theory, AutoDomainData]
    public async void CreateTruck_ReturnsFalse_WhenTruckDoesNotExist_AndSaveUnsuccessful(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, Truck truck)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync((Truck?)null);
        truckReposotory.Setup(r => r.Create(truck)).ReturnsAsync(false);

        /// Act
        var result = await sut.CreateTruck(truck);

        /// Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeFalse();
    }

    [Theory, AutoDomainData]
    public async void CreateTruck_ReturnsDuplicateTruckError_WhenTruckExists(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, Truck truck)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync(truck);

        /// Act
        var result = await sut.CreateTruck(truck);

        /// Assert
        result.FirstError.Code.Should().Be(Errors.DuplicateCode.Code);
    }

    [Theory, AutoDomainData]
    public async void GetTruck_ReturnsTruck_WhenTruckFound(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, Truck truck)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync(truck);

        /// Act
        var result = await sut.GetTruck(truck.Code);

        /// Assert
        result.Should().Be(truck);
    }

    [Theory, AutoDomainData]
    public async void GetTruck_ReturnsNull_WhenTruckNotFound(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, string truckCode)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truckCode)).ReturnsAsync((Truck?)null);

        /// Act
        var result = await sut.GetTruck(truckCode);

        /// Assert
        result.Should().BeNull();
    }
}