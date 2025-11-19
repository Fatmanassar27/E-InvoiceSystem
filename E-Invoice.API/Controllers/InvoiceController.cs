using E_Invoice.Application.DTOs;
using E_Invoice.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Invoice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoicesController : ControllerBase
    {
        private readonly IDocumentSubmissionService _submissionService;
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService, IDocumentSubmissionService submissionService)
        {
            _invoiceService = invoiceService;
            _submissionService = submissionService;
        }
        #region submit invoice

        [HttpPost]
        public async Task<IActionResult> SubmitInvoices([FromBody] List<InvoiceDto> invoices)
        {
            if (invoices == null || invoices.Count == 0)
                return BadRequest("No documents submitted.");

            var validator = HttpContext.RequestServices.GetRequiredService<IValidator<InvoiceDto>>();
            var failedInvoices = new List<(InvoiceDto Invoice, Dictionary<string, string[]> Errors)>();
            var validInvoices = new List<InvoiceDto>();

            foreach (var invoice in invoices)
            {
                var result = await validator.ValidateAsync(invoice);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    failedInvoices.Add((invoice, errors));
                }
                else
                {
                    validInvoices.Add(invoice);
                }
            }


            var response = await _submissionService.SubmitDocumentsAsync(validInvoices, failedInvoices);

            return Ok(response);
        }

        #endregion

        #region update invoice 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcceptedInvoice(int id, [FromBody] InvoiceDto invoiceDto)
        {
            if (invoiceDto == null)
                return BadRequest("Invoice data is required.");

            var existingInvoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (existingInvoice == null)
                return NotFound($"Invoice with ID {id} not found.");

            await _invoiceService.UpdateInvoiceAsync(id,invoiceDto);

            return Ok(invoiceDto);
        }

        #endregion

        #region Get Document

        #endregion
    }
}
