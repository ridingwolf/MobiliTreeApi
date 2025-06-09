using System;

namespace MobiliTreeApi.Sessions;

public record CreateSessionPayload(
    string ParkingFacilityId,
    string CustomerId,
    DateTime StartDateTime,
    DateTime EndDateTime);