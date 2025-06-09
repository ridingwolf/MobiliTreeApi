using MobiliTree.Domain.Models;

namespace MobiliTree.FakeData
{
    public static class SeedServiceProfile
    {
        public static Dictionary<string, ServiceProfile> GetAll() =>
            new Dictionary<string, ServiceProfile>
            {
                {
                    SeedFacilityId.Facility1,
                    new ServiceProfile
                    {
                        OverrunWeekDaysPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 7, 1.5m), // betweeen midnight and 7 AM price is 1.5 eur/hour
                            new TimeslotPrice(7, 18, 3.5m), // betweeen 7 AM and 6 PM price is 3.5 eur/hour
                            new TimeslotPrice(18, 24, 2.5m) // betweeen 6 PM and midnight price is 2.5 eur/hour
                        },
                        OverrunWeekendPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 7, 1.8m),
                            new TimeslotPrice(7, 18, 3.8m),
                            new TimeslotPrice(18, 24, 2.8m)
                        },
                        WeekDaysPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 7, 0.5m),
                            new TimeslotPrice(7, 18, 2.5m),
                            new TimeslotPrice(18, 24, 1.5m)
                        },
                        WeekendPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 7, 0.8m),
                            new TimeslotPrice(7, 18, 2.8m),
                            new TimeslotPrice(18, 24, 1.8m)
                        },
                    }
                },
                {
                    SeedFacilityId.Facility2,
                    new ServiceProfile
                    {
                        OverrunWeekDaysPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 8, 1.5m),
                            new TimeslotPrice(8, 17, 3.5m),
                            new TimeslotPrice(17, 24, 2.5m)
                        },
                        OverrunWeekendPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 8, 1.8m),
                            new TimeslotPrice(8, 17, 3.8m),
                            new TimeslotPrice(17, 24, 2.8m)
                        },
                        WeekDaysPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 8, 0.5m),
                            new TimeslotPrice(8, 17, 2.5m),
                            new TimeslotPrice(17, 24, 1.5m)
                        },
                        WeekendPrices = new List<TimeslotPrice>
                        {
                            new TimeslotPrice(0, 8, 0.8m),
                            new TimeslotPrice(8, 17, 2.8m),
                            new TimeslotPrice(17, 24, 1.8m)
                        },
                    }
                }
            };

        public static ServiceProfile For(string facilityId)
            => GetAll()[facilityId];
    }
}
