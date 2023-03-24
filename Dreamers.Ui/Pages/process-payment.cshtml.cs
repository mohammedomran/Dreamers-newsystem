using Dreamers.Ui.Dtos;
using Dreamers.Ui.Enums;
using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages
{
    public class process_paymentModel : PageModel
    {
        private readonly ExcursionRepo excursionRepo;
        private readonly IRazorPartialToStringRenderer razorPartialToStringRenderer;
        private readonly IEmailService emailService;

        [FromQuery]
        public PaymentResult PaymentResult { get; set; }

        public process_paymentModel(
            ExcursionRepo excursionRepo,
            IRazorPartialToStringRenderer razorPartialToStringRenderer,
            IEmailService emailService
        )
        {
            this.excursionRepo = excursionRepo;
            this.razorPartialToStringRenderer = razorPartialToStringRenderer;
            this.emailService = emailService;
        }

        public ActionResult OnGet()
        {
            if (PaymentResult.success)
            {
                excursionRepo.UpdateExcursionBooking(PaymentResult.bookingKey, (int)PaymenStatus.Accepted);
                var excursionBooking = excursionRepo.GetExcursionBooking(PaymentResult.bookingKey);
                sendConfirmationEmail(excursionBooking);

                return Redirect($"/receipt/{excursionBooking.Key}");
            }
            else
            {
                excursionRepo.UpdateExcursionBooking(PaymentResult.bookingKey, (int)PaymenStatus.Failed);

                return Redirect($"/payment-error");
            }
        }

        private void sendConfirmationEmail(Booking excursionBooking)
        {
            var excursionBookingDto = new ExcursionBookingDto
            {
                CheckIn = excursionBooking.CheckIn.ToString("dd-MM-yyyy"),
                Excursion = excursionBooking.Excursion.Title,
                FullName = excursionBooking.Name,
                Email = excursionBooking.Email,
                Adults = excursionBooking.AdultsNumber,
                Children = excursionBooking.AdultsNumber,
                Total = excursionBooking.TotalPrice,

            };
            var emailBody = razorPartialToStringRenderer.RenderPartialToStringAsync("MailTemplates/Excursion-booking", excursionBookingDto).Result;

            var emailDto = new EmailDto
            {
                ToEmail = excursionBookingDto.Email,
                FromEmail = "mohammedomran9512@gmail.com",
                Subject = "Excursion Receipt",
                Content = emailBody,
            };

            emailService.SendEmail(emailDto);
        }
    }

    public class PaymentResult
    {
        public bool success { get; set; }

        [FromQuery(Name = "merchant_order_id")]
        public string bookingKey { get; set; }

        [FromQuery(Name = "data.message")]
        public string message { get; set; }
    }

}
