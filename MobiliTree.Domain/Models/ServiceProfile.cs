namespace MobiliTree.Domain.Models
{
    public class ServiceProfile
    {
        public IList<TimeslotPrice> WeekDaysPrices { get; set; }
        public IList<TimeslotPrice> OverrunWeekDaysPrices { get; set; }
        public IList<TimeslotPrice> WeekendPrices { get; set; }
        public IList<TimeslotPrice> OverrunWeekendPrices { get; set; }

        public decimal GetPriceForStart(DateTime start)
        {
            var prices = start.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday 
                ? WeekendPrices
                : WeekDaysPrices;

            return prices
                .Single(price => price.StartHour <= start.Hour && start.Hour < price.EndHour)
                .PricePerHour;
        }
    }

    public class TimeslotPrice
    {
        public TimeslotPrice(int startHour, int endHour, decimal pricePerHour)
        {
            StartHour = startHour;
            EndHour = endHour;
            PricePerHour = pricePerHour;
        }

        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public decimal PricePerHour { get; set; }
    }
}