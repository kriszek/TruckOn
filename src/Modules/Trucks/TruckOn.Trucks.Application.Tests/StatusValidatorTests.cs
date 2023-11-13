using TruckOn.Trucks.Application.Tests.DataAttributes;

namespace TruckOn.Trucks.Application.Tests;

public class StatusValidatorTests
{
    [Theory]
    [MemberAutoDomainData(nameof(AllStatuses))]
    public void StatusIsProper_WhenSameStatuses(TruckStatus status, StatusValidator sut)
    {
        bool isProper = sut.IsNewStatusProper(status, status);

        isProper.Should().BeTrue();
    }

    [Theory]
    [MemberAutoDomainData(nameof(AllStatuses))]
    public void StatusIsProper_WhenCHangingFromOutOfService(TruckStatus status, StatusValidator sut)
    {
        bool isProper = sut.IsNewStatusProper(status, TruckStatus.OutOfService);

        isProper.Should().BeTrue();
    }

    [Theory]
    [MemberAutoDomainData(nameof(AllStatuses))]
    public void StatusIsProper_WhenCHangingToOutOfService(TruckStatus status, StatusValidator sut)
    {
        bool isProper = sut.IsNewStatusProper(TruckStatus.OutOfService, status);

        isProper.Should().BeTrue();
    }

    [Theory]
    [InlineAutoDomainData(TruckStatus.Loading, TruckStatus.ToJob)]
    [InlineAutoDomainData(TruckStatus.ToJob, TruckStatus.AtJob)]
    [InlineAutoDomainData(TruckStatus.AtJob, TruckStatus.Returning)]
    [InlineAutoDomainData(TruckStatus.Returning, TruckStatus.Loading)]
    public void StatusIsProper_WhenCHangingToProperState(TruckStatus oldStatus, TruckStatus newStatus, StatusValidator sut)
    {
        bool isProper = sut.IsNewStatusProper(newStatus, oldStatus);

        isProper.Should().BeTrue();
    }

    [Theory]
    [InlineAutoDomainData(TruckStatus.ToJob, TruckStatus.Loading)]
    [InlineAutoDomainData(TruckStatus.AtJob, TruckStatus.ToJob)]
    [InlineAutoDomainData(TruckStatus.Returning, TruckStatus.AtJob)]
    [InlineAutoDomainData(TruckStatus.Loading, TruckStatus.Returning)]
    public void StatusIsInproper_WhenCHangingToInproperState(TruckStatus oldStatus, TruckStatus newStatus, StatusValidator sut)
    {
        bool isProper = sut.IsNewStatusProper(newStatus, oldStatus);

        isProper.Should().BeFalse();
    }

    public static IEnumerable<object[]> AllStatuses()
    {
        foreach (var status in Enum.GetValues(typeof(TruckStatus)))
        {
            yield return new object[] { status };
        }
    }
}
