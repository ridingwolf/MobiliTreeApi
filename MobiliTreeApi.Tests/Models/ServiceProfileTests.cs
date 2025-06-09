using System;
using System.Collections.Generic;
using System.Linq;
using MobiliTree.Domain.Models;
using Xunit;

namespace MobiliTreeApi.Tests.Models;

public class GivenAServiceProfileWithASingleRatePerDay
{
    private readonly Random _random = new();
    private readonly ServiceProfile _sut;
    private readonly decimal _weekDayPrice;
    private readonly decimal _weekendDayPrice;

    public GivenAServiceProfileWithASingleRatePerDay()
    {
        _weekDayPrice = decimal.Divide(_random.Next(1, 100), 10);
        _weekendDayPrice = _weekDayPrice * 1.2m;
        
        _sut = new ServiceProfile
        {
            WeekDaysPrices = new List<TimeslotPrice> { new(0, 24, _weekDayPrice)},
            WeekendPrices = new List<TimeslotPrice> { new(0, 24, _weekendDayPrice)},
        };
    }

    [Fact]
    public void WhenGettingPriceForAWeekDay_ThenTheWeekDayPriceIsReturned()
    {
        var weekDay = GetRandomDate(DayOfWeek.Monday, DayOfWeek.Tuesday,  DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        Assert.Equal(_sut.GetPriceForStart(weekDay), _weekDayPrice);
    }
    
    [Fact]
    public void WhenGettingPriceForAWeekendDay_ThenTheWeekendDayPriceIsReturned()
    {
        var weekendDay = GetRandomDate(DayOfWeek.Saturday, DayOfWeek.Sunday);
        Assert.Equal(_sut.GetPriceForStart(weekendDay), _weekendDayPrice);
    }
    
    private DateTime GetRandomDate(params DayOfWeek[] allowedDays)
    {
        var randomDate = new DateTime(_random.Next(2000, 2050), 1, 1).AddDays(_random.Next(265));
        while (!allowedDays.Contains(randomDate.DayOfWeek))
            randomDate = randomDate.AddDays(1);
        
        return randomDate;
    }
}

public class GivenAServiceProfileWithSameRatesForAllDays
{
    private readonly Random _random = new();
    private readonly ServiceProfile _sut;
    private readonly decimal _morningPrice;
    private readonly decimal _eveningPrice;
    private readonly decimal _noonPrice;

    public GivenAServiceProfileWithSameRatesForAllDays()
    {
        _morningPrice = decimal.Divide(_random.Next(1, 100), 10);
        _noonPrice = _morningPrice * 1.2m;
        _eveningPrice = _morningPrice * 1.2m;

        var rates = new List<TimeslotPrice>
        {
            new(0, 10, _morningPrice),
            new(10, 14, _noonPrice),
            new(14, 24, _eveningPrice)
        };
        
        _sut = new ServiceProfile
        {
            WeekDaysPrices = rates,
            WeekendPrices = rates,
        };
    }

    [Fact]
    public void WhenGettingPriceForASessionStartingAtMidnight_ThenMorningPriceIsReturned()
    {
        var midnight = GetRandomDate(new TimeOnly(0, 0,0));
        Assert.Equal(_sut.GetPriceForStart(midnight), _morningPrice);
    }
    
    [Fact]
    public void WhenGettingPriceForASessionStartingDuringMorningTimeSlot_ThenMorningPriceIsReturned()
    {
        var morning = GetRandomDate(new TimeOnly(_random.Next(0, 10), _random.Next(0, 60),_random.Next(0, 60)));
        Assert.Equal(_sut.GetPriceForStart(morning), _morningPrice);
    }
    
    [Fact]
    public void WhenGettingPriceForASessionStartingExactlyOnSlotBoundry_ThenTheNextSlotPriceIsReturned()
    {
        var morning = GetRandomDate(new TimeOnly(10, 0,0));
        Assert.Equal(_sut.GetPriceForStart(morning), _noonPrice);
    }
    
    [Fact]
    public void WhenGettingPriceForASessionStartingDuringNoonTimeSlot_ThenNoonPriceIsReturned()
    {
        var morning = GetRandomDate(new TimeOnly(_random.Next(10, 14), _random.Next(0, 60),_random.Next(0, 60)));
        Assert.Equal(_sut.GetPriceForStart(morning), _noonPrice);
    }
    
    [Fact]
    public void WhenGettingPriceForASessionStartingDuringEveningTimeSlot_ThenEveningPriceIsReturned()
    {
        var morning = GetRandomDate(new TimeOnly(_random.Next(14, 24), _random.Next(0, 60),_random.Next(0, 60)));
        Assert.Equal(_sut.GetPriceForStart(morning), _eveningPrice);
    }
    
    private DateTime GetRandomDate(TimeOnly timeOnly)
        => new DateTime(_random.Next(2000, 2050), 1, 1, timeOnly.Hour, timeOnly.Minute, timeOnly.Second).AddDays(_random.Next(265));
}