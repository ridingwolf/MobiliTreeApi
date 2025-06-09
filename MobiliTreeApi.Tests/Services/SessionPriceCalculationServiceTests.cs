using System;
using System.Collections.Generic;
using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;
using MobiliTree.Domain.Services;
using MobiliTree.FakeData;
using Moq;
using Xunit;

namespace MobiliTreeApi.Tests.Services;

public class GivenAParkingSession
{
    private readonly Mock<IParkingFacilityRepository> _mockParkingFacilityRepository = new();
    private readonly SessionPriceCalculationService _sut;
    private readonly string _facilityId;
    private readonly decimal _configuredPricePerHour;
    private readonly Random _random = new(); 

    public GivenAParkingSession()
    {
        _facilityId = SeedFacilityId.Facility1;
        _configuredPricePerHour = 1.0m;
    
        _mockParkingFacilityRepository
            .Setup(repository => repository.GetServiceProfile(_facilityId))
            .Returns(new ServiceProfile
            {
                WeekDaysPrices = new List<TimeslotPrice>{ new(0, 24, _configuredPricePerHour)},
                WeekendPrices = new List<TimeslotPrice>{ new(0, 24, _configuredPricePerHour)},
            });

        _sut = new SessionPriceCalculationService(_mockParkingFacilityRepository.Object);
    }
    
    [Fact]
    public void ThenThePricePerHourIsMultipliedByTheNumberOfHoursParked()
    {
        var hours = _random.Next(1, 24);
        var start = DateTime.Now;
        var sessionPrice = _sut.CalculateSessionPriceFor(_facilityId, SeedCustomer.John.Id, start, start.AddHours(hours));
        
        Assert.Equal(_configuredPricePerHour * hours, sessionPrice);
    }
    
    [Fact]
    public void ThenThePricePerHourIsMultipliedByTheNumberOfStartedHoursParked()
    {
        var hours = _random.Next(1, 24);
        var start = DateTime.Now;
        var sessionPrice = _sut.CalculateSessionPriceFor(_facilityId, SeedCustomer.John.Id, start, start.AddHours(hours).AddTicks(1));
        
        Assert.Equal(_configuredPricePerHour * (hours + 1), sessionPrice);
    }
}