using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private static readonly String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("hxuu0069@student.monash.edu", "Hao");
            var to = new EmailAddress(toEmail, "Customer");
            // var to = new EmailAddress("brantleyxu@gmail.com", "Customer");
            var plainTextContent = contents;
            // var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<p>" + contents + "</p>";
            // var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            // subject = "Sending with SendGrid is Fun";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void Send_Async()
        {
            var apiKey = API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("hxuu0069@student.monash.edu", "Hao");
            var subject = "Auto Send by C# developed by Visual Studio";
            var to = new EmailAddress("wyqwyq32817@outlook.com", "Customer");
            var plainTextContent = "Hahahaha auto send, don't reply";
            var htmlContent = "<strong>easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

    }
}