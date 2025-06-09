namespace MobiliTree.Domain.Commands;

public record CreateSession(
    string ParkingFacilityId,
    string CustomerId,
    DateTime StartDateTime,
    DateTime EndDateTime);