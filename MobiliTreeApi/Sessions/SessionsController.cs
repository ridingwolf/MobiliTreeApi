using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MobiliTree.Domain.Repositories;
using MobiliTree.Domain.Services;
using MobiliTreeApi.Responses;

namespace MobiliTreeApi.Sessions
{
    [Route("sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionsRepository _sessionsRepository;
        private readonly ISessionService _service;

        public SessionsController(ISessionsRepository sessionsRepository, ISessionService service)
        {
            _sessionsRepository = sessionsRepository;
            _service = service;
        }

        [HttpPost]
        public ActionResult Post(CreateSessionPayload payload)
        {
            _service.CreateSession(new MobiliTree.Domain.Commands.CreateSession(payload.ParkingFacilityId, payload.CustomerId, payload.StartDateTime, payload.EndDateTime));
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
