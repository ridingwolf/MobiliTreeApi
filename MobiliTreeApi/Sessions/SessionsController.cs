using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MobiliTree.Domain.Repositories;
using MobiliTreeApi.Responses;

namespace MobiliTreeApi.Sessions
{
    [Route("sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionsRepository _sessionsRepository;

        public SessionsController(ISessionsRepository sessionsRepository)
        {
            _sessionsRepository = sessionsRepository;
        }

        [HttpPost]
        public ActionResult Post(CreateSession value)
        {
            _sessionsRepository.AddSession(
                new MobiliTree.Domain.Models.Session
                {
                    ParkingFacilityId = value.ParkingFacilityId,
                    CustomerId = value.CustomerId,
                    StartDateTime = value.StartDateTime,
                    EndDateTime = value.EndDateTime,
                });
            return Ok();
        }
        
        [HttpGet]
        [Route("{parkingFacilityId}")]
        public List<SessionResponse> Get(string parkingFacilityId)
        {
            return _sessionsRepository
                .GetSessions(parkingFacilityId)
                .Select(SessionResponse.Load)
                .ToList();
        }
    }
}
