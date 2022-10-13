using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NiceNeighbourPharmacy.Utils
{
    public class SendEmailUtil
    {
        private static readonly string API_KEY = "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";

        public static async Task Execute(string subject, string plainTextContent, string toEmail)
        {
            var apiKey = API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("BrantleyXU@gmail.com", "Hao");
            // var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(toEmail, "Example User");
            // var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}