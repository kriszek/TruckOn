using TruckOn.Trucks.Application.Tests.DataAttributes;

namespace TruckOn.Trucks.Application.Tests;

public class TrucksServiceTests
{
    [Theory, AutoDomainData]
    public async void UpsertTruck_ReturnsTrue_WhenTruckDoesNotExist_AndSaveSuccessful(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, Truck truck)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync((Truck?)null);
        truckReposotory.Setup(r => r.Create(truck)).ReturnsAsync(true);

        /// Act
        var result = await sut.UpsertTruck(truck);

        /// Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeTrue();
    }

    [Theory, AutoDomainData]
    public async void UpsertTruck_ReturnsFalse_WhenTruckDoesNotExist_AndSaveUnsuccessful(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut, Truck truck)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync((Truck?)null);
        truckReposotory.Setup(r => r.Create(truck)).ReturnsAsync(false);

        /// Act
        var result = await sut.UpsertTruck(truck);

        /// Assert
        result.FirstError.Should().Be(Errors.SaveFailed);
    }

    [Theory, AutoDomainData]
    public async void UpsertTruck_ReturnsTrue_WhenTruckExists_AndSaveSuccessful(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        [Frozen] Mock<IStatusValidator> statusValidator,
        TrucksService sut, Truck truck, Truck oldTruck)
    {
        /// Arrange
        truck.Code = oldTruck.Code;

        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync(oldTruck);
        truckReposotory.Setup(r => r.Update(oldTruck, truck)).ReturnsAsync(true);

        statusValidator.Setup(x => x.IsNewStatusProper(It.IsAny<TruckStatus>(), It.IsAny<TruckStatus>())).Returns(true);

        /// Act
        var result = await sut.UpsertTruck(truck);

        /// Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeFalse();
    }

    [Theory, AutoDomainData]
    public async void UpsertTruck_ReturnsError_WhenTruckExists_AndSaveUnsuccessful(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        [Frozen] Mock<IStatusValidator> statusValidator,
        TrucksService sut, Truck truck, Truck oldTruck)
    {
        /// Arrange
        truck.Code = oldTruck.Code;

        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync(oldTruck);
        truckReposotory.Setup(r => r.Update(oldTruck, truck)).ReturnsAsync(false);

        statusValidator.Setup(x => x.IsNewStatusProper(It.IsAny<TruckStatus>(), It.IsAny<TruckStatus>())).Returns(true);

        /// Act
        var result = await sut.UpsertTruck(truck);

        /// Assert
        result.FirstError.Should().Be(Errors.SaveFailed);
    }

    [Theory, AutoDomainData]
    public async void UpsertTruck_ReturnsError_WhenTruckExists_AndStatusNotProper(
        [Frozen] Mock<ITruckRepository> truckReposotory,
        [Frozen] Mock<IStatusValidator> statusValidator,
        TrucksService sut, Truck truck, Truck oldTruck)
    {
        /// Arrange
        truck.Code = oldTruck.Code;

        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync(oldTruck);

        statusValidator.Setup(x => x.IsNewStatusProper(It.IsAny<TruckStatus>(), It.IsAny<TruckStatus>())).Returns(false);

        /// Act
        var result = await sut.UpsertTruck(truck);

        /// Assert
        result.FirstError.Should().Be(Errors.InvalidStatus);
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