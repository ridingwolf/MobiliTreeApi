using MobiliTree.Domain.Commands;
using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;

namespace MobiliTree.Domain.Services;

public interface ISessionService
{
    void CreateSession(CreateSession command);
}

public class SessionService : ISessionService
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly ISessionPriceCalculationService _priceCalculationService;

    public SessionService(ISessionsRepository sessionsRepository, ISessionPriceCalculationService priceCalculationService)
    {
        _sessionsRepository = sessionsRepository;
        _priceCalculationService = priceCalculationService;
    }

    public void CreateSession(CreateSession command)
    {
        var cost = _priceCalculationService.CalculateSessionPriceFor(command.ParkingFacilityId, command.CustomerId, command.StartDateTime, command.EndDateTime);
        _sessionsRepository.AddSession(
            new Session
            {
                ParkingFacilityId = command.ParkingFacilityId,
                CustomerId = command.CustomerId,
                StartDateTime = command.StartDateTime,
                EndDateTime = command.EndDateTime,
                Cost = cost,
            });
    }
}