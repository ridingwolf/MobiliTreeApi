using System;

namespace MobiliTreeApi.Responses;

public record SessionResponse(string ParkingFacilityId, string CustomerId, DateTime StartDateTime, DateTime EndDateTime)
{
    public static SessionResponse Load(MobiliTree.Domain.Models.Session session)
        => new SessionResponse(session.ParkingFacilityId, session.CustomerId, session.StartDateTime, session.EndDateTime);
}
