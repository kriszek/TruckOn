namespace TruckOn.Trucks.Application.Tests.DataAttributes;

public class InlineAutoDomainDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoDomainDataAttribute(params object[] objects) : base(new AutoDomainDataAttribute(), objects) { }
}
