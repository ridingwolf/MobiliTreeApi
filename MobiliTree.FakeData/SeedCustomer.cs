using MobiliTree.Domain.Models;

namespace MobiliTree.FakeData;

public static class SeedCustomer
{
    public static readonly Customer John =  new("c001", "John", SeedFacilityId.Facility1);
    public static readonly Customer Sarah =  new("c002", "Sarah", SeedFacilityId.Facility1, SeedFacilityId.Facility2);
    public static readonly Customer Andrea =  new("c003", "Andrea", SeedFacilityId.Facility2);
    public static readonly Customer Peter =  new("c004", "Peter");
    
    public static Dictionary<string, Customer> GetAll() =>
        new[]
            {
                John,
                Sarah,
                Andrea,
                Peter
            }
            .ToDictionary(c => c.Id, c => c);
}