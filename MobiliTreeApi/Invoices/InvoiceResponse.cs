namespace MobiliTreeApi.Invoices;

public record InvoiceResponse(string ParkingFacilityId, string CustomerId, decimal Amount)
{
    public static InvoiceResponse Load(MobiliTree.Domain.Models.Invoice invoice)
        => new(
            invoice.ParkingFacilityId,
            invoice.CustomerId,
            invoice.Amount);
};