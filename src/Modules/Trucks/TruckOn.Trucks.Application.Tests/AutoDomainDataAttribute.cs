using AutoFixture.AutoMoq;

namespace TruckOn.Trucks.Application.Tests;

public class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
      : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}
