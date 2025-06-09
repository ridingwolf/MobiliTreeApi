using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MobiliTree.Domain.Models;
using MobiliTree.Domain.Services;

namespace MobiliTreeApi.Controllers
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
        public List<Invoice> Get(string parkingFacilityId)
        {
            return _invoiceService.GetInvoices(parkingFacilityId);
        }
    }
}
