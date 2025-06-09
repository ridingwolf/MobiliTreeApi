using MobiliTree.Domain.Models;

namespace MobiliTree.Domain.Repositories
{
    public interface IParkingFacilityRepository
    {
        ServiceProfile GetServiceProfile(string parkingFacilityId);
    }
}