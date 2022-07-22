using BlobService.Interface;
using BlobService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlobService.Service
{
    public class BlobService : IBlobService
    {
        private readonly CloudBlobContainer _cloudBlobContainer;
        private static string AzureBlobStorageConnectionString { get; set; }
        private static string CloudBlobContainerName { get; set; }
        public static IConfiguration _configuration { get; private set; }

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            CloudBlobContainerName = _configuration["Azure:Storage:CloudBlobContainerName"];
            // this._cloudBlobContainer = cloudBlobContainer;
            AzureBlobStorageConnectionString = _configuration["Azure:Storage:ConnectionString"];
            //  string connString = "the connection string from portal for your storage account, DefaultEndpointsProtocol=https;AccountName=storagetest789;AccountKey=G36m***==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobStorageConnectionString);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = cloudBlobClient.GetContainerReference(CloudBlobContainerName);

        }

        /// <summary>
        /// Uploads the given file to blob storage
        /// </summary>
        /// <param name="blob"></param>
        /// <returns>The url to the file uploaded</returns>
        #region Upload File to blob storage
        public async Task<string> Upload(IFormFile blob)
        {
            string url = "";
            byte[] blobBytes;

            try
            {
                if (blob.Length > 0)
                {
                    var fileName = GetFileName(blob);

                    var blobName = GenerateImageName(fileName);

                    blobBytes = ConvertImageToByteArray(blob);

                    var blobUrl = await UploadImageByteArray(
                        blobBytes,
                        blobName,
                        blob.ContentType);

                    url = blobUrl;

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return url;
        }

        private async Task<string> UploadImageByteArray(
           byte[] imageBytes,
           string imageName,
           string contentType)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null;
            }

            var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(imageName);

            cloudBlockBlob.Properties.ContentType = contentType;

            const int byteArrayStartIndex = 0;

            await cloudBlockBlob.UploadFromByteArrayAsync(
                imageBytes,
                byteArrayStartIndex,
                imageBytes.Length);

            var imageFullUrlPath = cloudBlockBlob.Uri.ToString();

            return imageFullUrlPath;
        }

        private byte[] ConvertImageToByteArray(IFormFile image)
        {
            byte[] result = null;

            using (var fileStream = image.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }

            return result;
        }

        private string GetFileName(IFormFile image)
        {
            var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition)
                        .FileName.Trim('"');

            return fileName;
        }

        private string GenerateImageName(string fileName)
        {
            var imageName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";

            return imageName;
        }

        #endregion

        #region Generate Booking Pdf
        /// <summary>
        /// Generates the booking pdf
        /// </summary>
        /// <param name="bookingPdfRequest"></param>
        /// <returns>The Url to the pdf generated</returns>
        public string GenerateTicketPdf(BookingPdfRequest bookingPdfRequest)
        {
            string pdfUrl = string.Empty;
            try
            {
                MemoryStream ms = new MemoryStream();
                //New document
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                XFont font10 = new XFont("Verdana", 10, XFontStyle.Regular);
                XFont font11 = new XFont("Verdana", 11, XFontStyle.Regular);
                XFont font13 = new XFont("Verdana", 13, XFontStyle.Regular);
                XFont font14 = new XFont("Verdana", 14, XFontStyle.Regular);
                XFont font9 = new XFont("Verdana", 9, XFontStyle.Regular);
                string cancellationStatus = bookingPdfRequest.IsCancelled ? "Cancelled" : "Not Cancelled";
                //PageSize 
                PdfDocument document = new PdfDocument();
                List<int> vatValues = new List<int>();
                PdfPage page = document.AddPage();
                //page.Size = PageSize.A4;
                double width = 831.49;
                double height = 1132;
                page.Width = new XUnit(width);
                page.Height = new XUnit(height);
                //page.Orientation = PageOrientation.Landscape;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                WebClient webClient = new WebClient();
                byte[] dataArr = webClient.DownloadData(bookingPdfRequest.AirlineLogo);

                using (MemoryStream stream = new MemoryStream(dataArr))
                {

                    XImage logo = XImage.FromStream(() => stream);
                    gfx.DrawImage(logo, 286, 37.79, 250, 132);
                }
                gfx.DrawString("PNR number: " + bookingPdfRequest.PnrNumber, font14, XBrushes.Black, new XPoint(94.48, 250));
                gfx.DrawString("Source: " + bookingPdfRequest.SourceLocation , font14, XBrushes.Black, new XPoint(94.48, 275));
                gfx.DrawString("Destination: " + bookingPdfRequest.DestinationLocation, font14, XBrushes.Black, new XPoint(94.48, 300));
                gfx.DrawString("Customer Name: " + bookingPdfRequest.CustomerName, font14, XBrushes.Black, new XPoint(94.48, 325));
                gfx.DrawString("Customer Email: " + bookingPdfRequest.CustomerEmailId, font14, XBrushes.Black, new XPoint(94.48, 350));
                gfx.DrawString("Travel Date:" + bookingPdfRequest.TravellingDate.ToString("dd-MMM-yyyy HH:mm tt"), font14, XBrushes.Black, new XPoint(94.48, 375));
                gfx.DrawString("Booked On:" + bookingPdfRequest.BookedOn.ToString("dd-MMM-yyyy HH:mm tt"), font14, XBrushes.Black, new XPoint(94.48, 400));
                gfx.DrawString("Total Seats Booked: " + bookingPdfRequest.NoOfSeats, font14, XBrushes.Black, new XPoint(94.48, 425));
                gfx.DrawString("Meal Preference: " + bookingPdfRequest.MealPlanType, font14, XBrushes.Black, new XPoint(94.48, 450));
                gfx.DrawString("Cancellation Status: " + cancellationStatus, font14, XBrushes.Black, new XPoint(94.48, 475));
                if (bookingPdfRequest.BookingPassengers != null && bookingPdfRequest.BookingPassengers.Count > 0)
                {
                    gfx.DrawString("Booking Passengers Details", new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(305, 525));

                gfx.DrawRectangle(new XSolidBrush((XColor.FromArgb(222, 222, 222))), new XRect(94.48, 575, 661.41, 30.39));
                gfx.DrawString("Name", font14, XBrushes.Black, new XPoint(101, 597.13));
                gfx.DrawString("Gender", font14, XBrushes.Black, new XPoint(220, 597.13));


                gfx.DrawString("Age", font14, XBrushes.Black, new XPoint(344, 597.13));


                gfx.DrawString("Seat No", font14, XBrushes.Black, new XPoint(460, 597.13));
                gfx.DrawString("Seat Type", font14, XBrushes.Black, new XPoint(600, 597.13));

                int currentYaxis = 627;
                
                    for (int i = 0; i < bookingPdfRequest.BookingPassengers.Count(); i++)
                    {
                        var passenger = bookingPdfRequest.BookingPassengers[i];
                        gfx.DrawString(passenger.PassengerName, font10, XBrushes.Black, new XPoint(101, currentYaxis));
                        gfx.DrawString(passenger.GenderType, font14, XBrushes.Black, new XPoint(220, currentYaxis));
                        gfx.DrawString(passenger.PassengerAge.ToString(), font14, XBrushes.Black, new XPoint(344, currentYaxis));
                        gfx.DrawString(passenger.SeatNo, font14, XBrushes.Black, new XPoint(470, currentYaxis));
                        gfx.DrawString(passenger.IsBusinessSeat ? "Business" : "Regular", font14, XBrushes.Black, new XPoint(600, currentYaxis));
                        currentYaxis = currentYaxis + 20;
                        gfx.DrawLine(new XPen(XColor.FromArgb(112, 112, 112)), new XPoint(94.48, currentYaxis), new XPoint(755.9, currentYaxis));
                        currentYaxis = currentYaxis + 30;

                    }

                    gfx.DrawString("Total Booking Cost: Rs " + Math.Round(bookingPdfRequest.TotalCost,2).ToString(), new XFont("Verdana", 16, XFontStyle.Bold), XBrushes.Black, new XPoint(101, currentYaxis));
                    if (bookingPdfRequest.IsCancelled)
                    {
                        gfx.DrawString("Warning: This ticket is cancelled and stands inactive", new XFont("Verdana", 24, XFontStyle.Underline), XBrushes.Red, new XPoint(101, currentYaxis + 50));
                    }
                }

                gfx.DrawString("For any queries please call our toll free number 9876543210", new XFont("Verdana", 14, XFontStyle.Underline), XBrushes.Black, new XPoint(101, 1000));
                gfx.DrawString("You can also email to us at support@flightmanagement.com", new XFont("Verdana", 14, XFontStyle.Underline), XBrushes.Black, new XPoint(101, 1030));
                gfx.DrawString("Please provide your feedback on the service at feedback@flightmanagement.com", new XFont("Verdana", 14, XFontStyle.Underline), XBrushes.Black, new XPoint(101, 1060));

                //document.Save("D:\\DummyPdf.pdf");
                document.Save(ms);
                pdfUrl = UploadPdf(ms.ToArray(), "Booking_" + bookingPdfRequest.PnrNumber);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return pdfUrl;
        }
        #endregion

        #region Upload Pdf
        /// <summary>
        /// Method which uploads the booking pdf and returns the blob link.
        /// </summary>
        /// <param name="pdf"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string UploadPdf(byte[] pdf, string fileName)
        {
            string url = string.Empty;
            try
            {
                var blobUrl = UploadImageByteArray(
                        pdf,
                        fileName,
                        "application/pdf");

                url = blobUrl.Result;
            }
            catch (Exception ex)
            {

            }

            return url;
        }
        #endregion

    }
}
