using Dreamers.Ui.Dtos;
using ElasticEmail.Model;

namespace Dreamers.Ui.Infrastructure
{
    public interface IEmailService
    {
        public EmailSend SendEmail(EmailDto emailDto);
    }
}
