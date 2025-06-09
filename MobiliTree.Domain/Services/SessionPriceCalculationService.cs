using MobiliTree.Domain.Repositories;

namespace MobiliTree.Domain.Services;

public interface ISessionPriceCalculationService
{
    decimal CalculateSessionPriceFor(string facilityId, string customerId, DateTime start, DateTime end);
}

public class SessionPriceCalculationService(IParkingFacilityRepository parkingFacilityRepository) : ISessionPriceCalculationService
{
    private readonly IParkingFacilityRepository _parkingFacilityRepository = parkingFacilityRepository;

    public decimal CalculateSessionPriceFor(string facilityId, string customerId, DateTime start, DateTime end)
    {
        var pricePerHour = _parkingFacilityRepository
            .GetServiceProfile(facilityId)
            .GetPriceForStart(start);
        
        var startedHours = Math.Ceiling(end.Subtract(start).TotalHours);
        return pricePerHour *  new decimal(startedHours);
    }
}

