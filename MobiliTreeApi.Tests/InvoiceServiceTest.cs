using System;
using System.Linq;
using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;
using MobiliTree.Domain.Services;
using MobiliTree.FakeData;
using MobiliTree.FakeData.Repositories;
using Xunit;

namespace MobiliTreeApi.Tests
{
    public class InvoiceServiceTest
    {
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IParkingFacilityRepository _parkingFacilityRepository;
        private readonly ICustomerRepository _customerRepository;

        public InvoiceServiceTest()
        {
            _sessionsRepository = new SessionsRepositoryFake(SeedSessions.GetAll());
            _parkingFacilityRepository = new ParkingFacilityRepositoryFake(SeedServiceProfile.GetAll());
            _customerRepository = new CustomerRepositoryFake(SeedCustomer.GetAll());
        }

        [Fact]
        public void GivenSessionsService_WhenQueriedForInexistentParkingFacility_ThenThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => GetSut().GetInvoices("nonExistingParkingFacilityId"));
            Assert.Equal("Invalid parking facility id 'nonExistingParkingFacilityId'", ex.Message);
        }

        [Fact]
        public void GivenEmptySessionsStore_WhenQueriedForUnknownParkingFacility_ThenReturnEmptyInvoiceList()
        {
            var result = GetSut().GetInvoices(SeedFacilityId.Facility1);

            Assert.Empty(result);
        }

        [Fact]
        public void GivenOneSessionInTheStore_WhenQueriedForExistingParkingFacility_ThenReturnInvoiceListWithOneElement()
        {
            var startDateTime = new DateTime(2018, 12, 15, 12, 25, 0);
            var facilityId = SeedFacilityId.Facility1;
            
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = "some customer",
                ParkingFacilityId = facilityId,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });

            var result = GetSut().GetInvoices(facilityId);
            
            var invoice = Assert.Single(result);
            Assert.NotNull(invoice);
            Assert.Equal(facilityId, invoice.ParkingFacilityId);
            Assert.Equal("some customer", invoice.CustomerId);
        }

        [Fact]
        public void GivenMultipleSessionsInTheStore_WhenQueriedForExistingParkingFacility_ThenReturnOneInvoicePerCustomer()
        {
            var startDateTime = new DateTime(2018, 12, 15, 12, 25, 0);
            var facilityId = SeedFacilityId.Facility1;
            var customerId = SeedCustomer.John.Id;
            var otherCustomerId = SeedCustomer.Sarah.Id;
            
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = customerId,
                ParkingFacilityId = facilityId,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = customerId,
                ParkingFacilityId = facilityId,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = otherCustomerId,
                ParkingFacilityId = facilityId,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });

            var result = GetSut().GetInvoices(facilityId);

            Assert.Equal(2, result.Count);
            var invoiceCust1 = result.SingleOrDefault(x => x.CustomerId == customerId);
            var invoiceCust2 = result.SingleOrDefault(x => x.CustomerId == otherCustomerId);
            Assert.NotNull(invoiceCust1);
            Assert.NotNull(invoiceCust2);
            Assert.Equal(facilityId, invoiceCust1.ParkingFacilityId);
            Assert.Equal(facilityId, invoiceCust2.ParkingFacilityId);
            Assert.Equal(customerId, invoiceCust1.CustomerId);
            Assert.Equal(otherCustomerId, invoiceCust2.CustomerId);
        }

        [Fact]
        public void GivenMultipleSessionsForMultipleFacilitiesInTheStore_WhenQueriedForExistingParkingFacility_ThenReturnInvoicesOnlyForQueriedFacility()
        {
            var startDateTime = new DateTime(2018, 12, 15, 12, 25, 0);
            var requestedFacilityId = SeedFacilityId.Facility1;
            var customerId = SeedCustomer.John.Id;
            var otherCustomerId = SeedCustomer.Sarah.Id;
            
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = customerId,
                ParkingFacilityId = requestedFacilityId,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = customerId,
                ParkingFacilityId = SeedFacilityId.Facility2,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });
            _sessionsRepository.AddSession(new Session
            {
                CustomerId = otherCustomerId,
                ParkingFacilityId = requestedFacilityId,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime.AddHours(1)
            });

            var result = GetSut().GetInvoices(requestedFacilityId);

            Assert.Equal(2, result.Count);
            var invoiceCust1 = result.SingleOrDefault(x => x.CustomerId == customerId);
            var invoiceCust2 = result.SingleOrDefault(x => x.CustomerId == otherCustomerId);
            Assert.NotNull(invoiceCust1);
            Assert.NotNull(invoiceCust2);
            Assert.Equal(requestedFacilityId, invoiceCust1.ParkingFacilityId);
            Assert.Equal(requestedFacilityId, invoiceCust2.ParkingFacilityId);
            Assert.Equal(customerId, invoiceCust1.CustomerId);
            Assert.Equal(otherCustomerId, invoiceCust2.CustomerId);
        }

        private IInvoiceService GetSut()
        {
            return new InvoiceService(
                _sessionsRepository, 
                _parkingFacilityRepository,
                _customerRepository);
        }
    }
}
