using System;
using MobiliTree.Domain.Commands;
using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;
using MobiliTree.Domain.Services;
using MobiliTree.FakeData;
using Moq;
using Xunit;

namespace MobiliTreeApi.Tests;

public class GivenACreateSessionCommand
{
    private readonly Mock<ISessionsRepository> _repositoryMock = new();
    private readonly Mock<ISessionPriceCalculationService> _sessionPriceCalculationServiceMock  = new();
    
    private readonly CreateSession _createSession;
    private readonly decimal _price;
    
    public GivenACreateSessionCommand()
    {
        _createSession = new CreateSession(
            SeedFacilityId.Facility1,
            SeedCustomer.John.Id,
            DateTime.Now.AddMinutes(-145),
            DateTime.Now);
        
        _price = decimal.Divide(new Random().Next(0, 100), 10);

        _repositoryMock
            .Setup(repository => repository.AddSession(It.IsAny<Session>()));
        
        _sessionPriceCalculationServiceMock
            .Setup(service => service.CalculateSessionPriceFor(_createSession.ParkingFacilityId, _createSession.CustomerId, _createSession.StartDateTime, _createSession.EndDateTime))
            .Returns(_price);
        
        var sut = new SessionService(_repositoryMock.Object, _sessionPriceCalculationServiceMock.Object);
        
        sut.CreateSession(_createSession);
    }

    [Fact]
    public void ThenASessionWithTheCalculatedPriceShouldSaved()
    {
        _repositoryMock.Verify(repository => repository.AddSession(
            It.Is<Session>(session => 
                session.ParkingFacilityId == _createSession.ParkingFacilityId
                && session.CustomerId == _createSession.CustomerId
                && session.StartDateTime == _createSession.StartDateTime
                && session.EndDateTime == _createSession.EndDateTime
                && session.Cost == _price)),
            Times.Once);
    }
}