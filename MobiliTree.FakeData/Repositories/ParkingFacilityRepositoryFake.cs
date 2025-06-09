using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;

namespace MobiliTree.FakeData.Repositories
{
    public class ParkingFacilityRepositoryFake : IParkingFacilityRepository
    {
        private readonly Dictionary<string, ServiceProfile> _serviceProfiles;

        public ParkingFacilityRepositoryFake(Dictionary<string, ServiceProfile> serviceProfiles)
        {
            _serviceProfiles = serviceProfiles;
        }

        public ServiceProfile GetServiceProfile(string parkingFacilityId)
        {
            if (_serviceProfiles.TryGetValue(parkingFacilityId, out var serviceProfile))
            {
                return serviceProfile;    
            }

            return null;
        }
    }
}
