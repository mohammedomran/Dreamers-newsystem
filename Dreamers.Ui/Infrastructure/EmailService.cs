using System;
using System.Collections.Generic;
using Dreamers.Ui.Dtos;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;

namespace Dreamers.Ui.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public EmailSend SendEmail(EmailDto emailDto)
        {
            var apikey = configuration.GetValue<string>("elasticmailapikey");

            Configuration config = new Configuration();
            // Configure API key authorization: apikey
            config.ApiKey.Add("X-ElasticEmail-ApiKey", apikey);

            var apiInstance = new EmailsApi(config);

            var to = new List<string>();
            to.Add(emailDto.ToEmail);
            var recipients = new TransactionalRecipient(to: to);
            EmailTransactionalMessageData emailData = new EmailTransactionalMessageData(recipients: recipients);
            emailData.Content = new EmailContent();
            emailData.Content.Body = new List<BodyPart>();
            BodyPart htmlBodyPart = new BodyPart();
            htmlBodyPart.ContentType = BodyContentType.HTML;
            htmlBodyPart.Charset = "utf-8";
            htmlBodyPart.Content = emailDto.Content;
            BodyPart plainTextBodyPart = new BodyPart();
            plainTextBodyPart.ContentType = BodyContentType.PlainText;
            plainTextBodyPart.Charset = "utf-8";
            plainTextBodyPart.Content = "Mail content";
            emailData.Content.Body.Add(htmlBodyPart);
            emailData.Content.Body.Add(plainTextBodyPart);
            emailData.Content.From = emailDto.FromEmail;
            emailData.Content.Subject = emailDto.Subject;

            try
            {
                return apiInstance.EmailsTransactionalPost(emailData);
            }
            catch (ApiException e)
            {
                Console.WriteLine("Exception when calling EmailsApi.EmailsTransactionalPost: " + e.Message);
                Console.WriteLine("Status Code: " + e.ErrorCode);
                Console.WriteLine(e.StackTrace);
                throw;
            }

        }
    }
}
