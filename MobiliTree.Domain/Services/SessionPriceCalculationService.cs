namespace MobiliTree.Domain.Services;

public interface ISessionPriceCalculationService
{
    decimal CalculateSessionPriceFor(string facilityId, string customerId, DateTime start, DateTime end);
}

public class SessionPriceCalculationService : ISessionPriceCalculationService
{
    public decimal CalculateSessionPriceFor(string facilityId, string customerId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}

