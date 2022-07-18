using BlobService.Interface;
using BlobService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/blob")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;

        public BlobController(IBlobService blobService, ILogger<BlobController> logger)
        {
            _blobService = blobService;
            _logger = logger;
        }

        #region Upload File
        [HttpPost]
        public async Task<IActionResult> AddBlob(IFormFile blob)
        {
            try
            {
                var url = await _blobService.Upload(blob);
                return Ok(new { url = url });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, new { errorMessage = ex.Message });
            }
            

        }
        #endregion

        #region Generate booking Pdf
        [HttpPost, Route("generatepdf")]
        public IActionResult GenerateBookingPdf(BookingPdfRequest bookingPdfRequest)
        {
            try
            {
                string pdfUrl = _blobService.GenerateTicketPdf(bookingPdfRequest);
                return Ok(new { url = pdfUrl });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, new { errorMessage = ex.Message });
            }
        }
        #endregion
    }
}
