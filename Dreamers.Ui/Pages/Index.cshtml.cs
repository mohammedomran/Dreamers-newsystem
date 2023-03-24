using Dreamers.Ui.Dtos;
using Dreamers.Ui.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dreamers.Ui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmailService emailService;
        private readonly IRazorPartialToStringRenderer razorPartialToStringRenderer;

        public IndexModel(IEmailService emailService, IRazorPartialToStringRenderer razorPartialToStringRenderer)
        {
            this.emailService = emailService;
            this.razorPartialToStringRenderer = razorPartialToStringRenderer;
        }

        public void OnGet()
        {
            var excursionBooking = new ExcursionBookingDto { 
                CheckIn = DateTime.Now.ToString(),
                Excursion = "Cairo Pyramids",
                FullName = "Karim Mohammed",
                Adults = 1,
                Children = 2,
                AdultPrice = 50,
                ChildrenPrice = 25,
                Total = 50,

            };
            var emailBody = razorPartialToStringRenderer.RenderPartialToStringAsync("MailTemplates/Excursion-booking", excursionBooking).Result;

            var emailDto = new EmailDto
            {
                ToEmail = "mohammedomran312@gmail.com",
                FromEmail = "mohammedomran9512@gmail.com",
                Subject = "Excursion Receipt",
                Content = emailBody,
            };

            //emailService.SendEmail(emailDto);
        }

    }
}