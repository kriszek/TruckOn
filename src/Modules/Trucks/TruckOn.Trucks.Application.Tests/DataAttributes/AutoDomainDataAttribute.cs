using AutoFixture.AutoMoq;

namespace TruckOn.Trucks.Application.Tests.DataAttributes;

public class AutoDomainDataAttribute : AutoDataAttribute
{
  public AutoDomainDataAttribute()
    : base(() => new Fixture().Customize(new AutoMoqCustomization()))
  {
  }
}
