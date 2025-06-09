using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MobiliTree.Domain.Services;

namespace MobiliTreeApi.Invoices
{
    [Route("invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("ping")]
        public string Get()
        {
            return "pong!";
        }

        [HttpGet]
        [Route("{parkingFacilityId}")]
        public List<InvoiceResponse> Get(string parkingFacilityId)
        {
            return _invoiceService
                .GetInvoices(parkingFacilityId)
                .Select(InvoiceResponse.Load)
                .ToList();
        }
    }
}
