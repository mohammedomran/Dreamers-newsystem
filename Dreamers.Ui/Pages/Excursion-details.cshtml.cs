using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Payment;

namespace Dreamers.Ui.Pages
{
    public class Excursion_detailsModel : PageModel
    {

        private readonly ExcursionRepo excursionRepo;
        private readonly IPaymobCashInBroker broker;
        private readonly IOptions<PaymobConfiguration> paymobOptions;

        public Excursion Excursion { get; set; }
        [BindProperty]
        public Booking ExcursionBookingModel { get; set; } = new Booking();

        public Excursion_detailsModel(
            ExcursionRepo excursionRepo,
            IPaymobCashInBroker broker,
            IOptions<PaymobConfiguration> paymobOptions
        )
        {
            this.excursionRepo = excursionRepo;
            this.broker = broker;
            this.paymobOptions = paymobOptions;
        }

        public ActionResult OnGet(string excursionUrlName)
        {
            var excursionUrl = Request.Path.ToString()?.Split("/").LastOrDefault();
            Excursion = excursionRepo.GetExcursion(excursionUrl);
            if (Excursion is null)
            {
                return Redirect("/error");
            }
            return Page();
        }

        public ActionResult OnPost(string excursionUrlName)
        {
            var excursionUrl = Request.Path.ToString()?.Split("/").LastOrDefault();
            Excursion = excursionRepo.GetExcursion(excursionUrl);
            var bookingKey = generateOrderKey();
            ExcursionBookingModel.Key = bookingKey.ToString();
            ExcursionBookingModel.ExcursionId = Excursion.Id;
            ExcursionBookingModel.DateCreated = DateTime.Now;
            ExcursionBookingModel.TotalPrice = calculateTotalPrice();
            excursionRepo.StoreExcursionBooking(ExcursionBookingModel);

            var paymentKeyRequest = new CashInPaymentKeyRequest(
                    integrationId: 2269842,
                    orderId: bookingKey,
                    billingData: new CashInBillingData("mo", "salah", "012345789", "test@g.com"),
                    amountCents: 1);

            var intergrationId = paymobOptions.Value.TestMode ? paymobOptions.Value.TestIntegrationId : paymobOptions.Value.LiveIntegrationId;
            PaymobService paymobService = new PaymobService(broker, intergrationId, paymobOptions);

            var result = paymobService.GetPaymentIFrameSrc(
                bookingKey.ToString(),
                "mo",
                "omran",
                "01234568979",
                "m@test.com",
                10
            );

            return Redirect(result);
        }

        private decimal calculateTotalPrice()
        {
            return ExcursionBookingModel.AdultsNumber * Excursion.Price +
                   (ExcursionBookingModel.ChildrenNumber * (Excursion.Price / 2));
        }

        private int generateOrderKey()
        {
            return new Random().Next(1_000_000, 9_999_999);
        }


        public string ConvertToEmbedLink(string youtubeLink)
        {
            // Define the regular expression pattern to match YouTube links
            string pattern = @"^(https:\/\/)?(www\.)?(youtube\.com\/watch\?v=|youtu\.be\/)([\w-]{11})";

            // Create a regex object with the pattern
            Regex regex = new Regex(pattern);

            // Match the input YouTube link against the pattern
            Match match = regex.Match(youtubeLink);

            // If a match is found, extract the video ID and construct the embed link
            if (match.Success)
            {
                string videoId = match.Groups[4].Value;
                string embedLink = $"https://www.youtube.com/embed/{videoId}";
                return embedLink;
            }

            // If no match is found, return null or an empty string, or throw an exception, depending on your desired behavior
            return null;
        }


    }
}
