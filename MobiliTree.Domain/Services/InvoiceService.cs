using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;

namespace MobiliTree.Domain.Services
{
    public interface IInvoiceService
    {
        List<Invoice> GetInvoices(string parkingFacilityId);
        Invoice GetInvoice(string parkingFacilityId, string customerId);
    }

    public class InvoiceService: IInvoiceService
    {
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IParkingFacilityRepository _parkingFacilityRepository;
        private readonly ICustomerRepository _customerRepository;

        public InvoiceService(ISessionsRepository sessionsRepository, IParkingFacilityRepository parkingFacilityRepository, ICustomerRepository customerRepository)
        {
            _sessionsRepository = sessionsRepository;
            _parkingFacilityRepository = parkingFacilityRepository;
            _customerRepository = customerRepository;
        }

        public List<Invoice> GetInvoices(string parkingFacilityId)
        {
            var serviceProfile = _parkingFacilityRepository.GetServiceProfile(parkingFacilityId);
            if (serviceProfile == null)
            {
                throw new ArgumentException($"Invalid parking facility id '{parkingFacilityId}'");
            }

            var sessions = _sessionsRepository.GetSessions(parkingFacilityId);

            return sessions
                .GroupBy(session => session.CustomerId)
                .Select(sessionForCustomer =>
                    new Invoice
                    {
                        ParkingFacilityId = parkingFacilityId,
                        CustomerId = sessionForCustomer.Key,
                        Amount = sessionForCustomer.Sum(session => session.Cost)
                    })
                .ToList();
        }

        public Invoice GetInvoice(string parkingFacilityId, string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
