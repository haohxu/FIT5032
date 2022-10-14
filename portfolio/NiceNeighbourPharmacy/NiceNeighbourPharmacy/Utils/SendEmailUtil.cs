using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NiceNeighbourPharmacy.Utils
{
    public class SendEmailUtil
    {
        private static readonly string API_KEY = "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";

        public static void Execute(string subject, string textContents, string toEmail, string attachmentPath = null)
        {
            var apiKey = API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("BrantleyXU@gmail.com", "Hao");
            // var subject = "";
            var to = new EmailAddress(toEmail, "Example User");
            var plainTextContent = "";
            var htmlContent = "<strong>" + textContents + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if (attachmentPath != null && attachmentPath.Length > 0)
            {
                string base64String = Convert.ToBase64String(File.ReadAllBytes(attachmentPath));
                msg.AddAttachment(Path.GetFileName(attachmentPath), base64String);
            }
            var response = client.SendEmailAsync(msg);
        }
    }
}