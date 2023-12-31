using TruckOn.Trucks.Application.Tests.DataAttributes;
using TruckOn.Trucks.Models.QueryFilters;

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

    [Theory, AutoDomainData]
    public async void GetTrucks_CallsRepository_AndReturnsResults(
        IEnumerable<IQueryFilter<Truck>> filters,
        PageResult<Truck> repositoryResult,
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut)
    {
        /// Arrange
        var method = truckReposotory.Setup(r => r.GetTrucks(filters));
        method.ReturnsAsync(repositoryResult);
        method.Verifiable(Times.Exactly(1));

        /// Act
        var result = await sut.GetTrucks(filters);

        /// Assert
        result.Should().Be(repositoryResult);
        truckReposotory.Verify();
    }

    [Theory, AutoDomainData]
    public async void DeleteTruck_ReturnsNotFOund_WhenNoTruckInRepository(
        Truck truck,
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync((Truck?)null);

        /// Act
        var result = await sut.DeleteTruck(truck.Code);

        /// Assert
        result.FirstError.Should().Be(Errors.NotFound);
    }

    [Theory]
    [InlineAutoDomainData(true)]
    [InlineAutoDomainData(false)]
    public async void DeleteTruck_ReturnsResultFromRepository_WhenTruckFound(
        bool deletionResult,
        Truck truck,
        [Frozen] Mock<ITruckRepository> truckReposotory,
        TrucksService sut)
    {
        /// Arrange
        truckReposotory.Setup(r => r.GetTruck(truck.Code)).ReturnsAsync(truck);
        truckReposotory.Setup(r => r.Delete(truck)).ReturnsAsync(deletionResult);

        /// Act
        var result = await sut.DeleteTruck(truck.Code);

        /// Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(deletionResult);
    }
}