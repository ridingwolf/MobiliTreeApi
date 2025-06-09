using System;

namespace MobiliTreeApi.Sessions;

public record CreateSession(
    string ParkingFacilityId,
    string CustomerId,
    DateTime StartDateTime,
    DateTime EndDateTime);