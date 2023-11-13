using Xunit.Sdk;

namespace TruckOn.Trucks.Application.Tests.DataAttributes;

public class MemberAutoDomainDataAttribute : CompositeDataAttribute
{
    public MemberAutoDomainDataAttribute(string memberName, params object[] parameters)
        : base(new DataAttribute[] {
            new MemberDataAttribute(memberName, parameters),
            new AutoDomainDataAttribute()})
    {

    }
}
