using BlobService.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobService.Interface
{
    public interface IBlobService
    {
        Task<string> Upload(IFormFile blob);
        string GenerateTicketPdf(BookingPdfRequest bookingPdfRequest);
    }
}
